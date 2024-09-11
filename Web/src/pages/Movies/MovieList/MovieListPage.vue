<script>
import { getGenres, searchMovies } from "@/api"
import ExtraListContent from "@/components/ExtraListContent.vue"
import LoadingOverlay from "@/components/LoadingOverlay.vue"
import PageCounter from "@/components/PageCounter.vue"
import PageSizeSelector from "@/components/PageSizeSelector.vue"
import { debounce, delay } from "@/utils"
import CreateMovieModal from "./CreateMovieModal.vue"
import DeleteMovieModal from "./DeleteMovieModal.vue"
import EditMovieModal from "./EditMovieModal.vue"
import MoviesTable from "./MoviesTable.vue"

const DEBOUNCE_TIME = 500

export default {
	components: {
		LoadingOverlay,
		PageSizeSelector,
		MoviesTable,
		CreateMovieModal,
		EditMovieModal,
		DeleteMovieModal,
		PageCounter,
		ExtraListContent,
	},
	data() {
		return {
			movies: [],
			genres: [],
			pageSize: Number.parseInt(localStorage.getItem("moviePageSize")) ?? 5,
			pageNumber: 1,
			totalPages: 0,
			totalItems: 0,
			isLoaded: false,
			firstLoad: true,
			createModalOpen: false,
			editMovie: null,
			deleteMovie: null,
			sortValue: 0,
			sortDirectionAsc: true,
			genreSearch: null,
			titleSearch: "",
			releaseSearch: "",
			debouncedSearch: debounce(this.excecuteSearchMovies, DEBOUNCE_TIME),
		}
	},
	async mounted() {
		this.excecuteGetGenres()
		this.excecuteSearchMovies()
	},
	methods: {
		findMovieById(id) {
			const filteredMovies = this.movies.filter((movie) => {
				return movie.id === id
			})
			if (filteredMovies.length === 1) {
				return filteredMovies[0]
			}
			if (filteredMovies.length === 0) {
				throw new Error("Movie not found")
			}
			throw new Error(`Multiple movies found with id ${id}`)
		},
		async excecuteSearchMovies() {
			//console.log(this.genreSearch.id)
			localStorage.setItem("moviePageSize", this.pageSize)
			this.isLoaded = false
			try {
				await delay(750)
				let response = await searchMovies({
					page: this.pageNumber,
					pageSize: this.pageSize,
					title: this.titleSearch || null,
					genreIds: this.genreSearch === null ? null : [this.genreSearch],
					// genres: null,
					releasedAfter: this.releaseSearch || null,
					sortType: this.sortValue,
					sortDirection: this.sortDirectionAsc ? 0 : 1,
				})
				this.movies = response.movies
				this.totalPages = response.pageCount
				this.totalItems = response.movieCount
				this.firstLoad = false
			} catch (error) {
				alert(error)
			} finally {
				this.isLoaded = true
			}
		},
		async excecuteGetGenres() {
			try {
				let response = await getGenres()
				this.genres = response
			} catch (error) {
				alert(error)
			}
		},
	},
	watch: {
		titleSearch(newTitleSearch) {
			this.debouncedSearch(newTitleSearch)
		},
		releaseSearch() {
			this.excecuteSearchMovies()
		},
		pageNumber() {
			this.excecuteSearchMovies()
		},
	},
}
</script>

<template>
	<h1>Movie List</h1>
	<p>
		<input @click="createModalOpen = true" class="btn btn-primary" type="submit" value="Create New" />
	</p>
	<form style="display: flex; gap: 1rem" onsubmit="event.preventDefault();">
		<PageSizeSelector
			:page-size="pageSize"
			@sizeChange="
				(newValue) => {
					pageSize = newValue
					excecuteSearchMovies()
				}
			"
		/>
		<p>
			Genre:
			<select v-model="genreSearch" @click="excecuteGetGenres" @change="excecuteSearchMovies">
				<option :value="null">All</option>
				<option v-for="genre in genres" :value="genre.id">{{ genre.name }}</option>
			</select>
		</p>
		<p>Title: <input type="text" v-model="titleSearch" /></p>
		<p>Released After: <input type="date" v-model="releaseSearch" /></p>
	</form>
	<CreateMovieModal
		@modalCancel="
			() => {
				createModalOpen = false
			}
		"
		@movieCreated="
			() => {
				createModalOpen = false
				excecuteSearchMovies()
			}
		"
		v-if="createModalOpen"
	/>
	<EditMovieModal
		:selected-movie="editMovie"
		@modalCancel="
			() => {
				editMovie = null
			}
		"
		@movieEdited="
			() => {
				editMovie = null
				excecuteSearchMovies()
			}
		"
		v-if="editMovie !== null"
	/>
	<DeleteMovieModal
		:selected-movie="deleteMovie"
		@modalCancel="() => (deleteMovie = null)"
		@movieDeleted="
			() => {
				deleteMovie = null
				excecuteSearchMovies()
			}
		"
		v-if="deleteMovie !== null"
	/>
	<LoadingOverlay v-if="!isLoaded" />
	<MoviesTable
		@sortChanged="
			(sortInfo) => {
				sortValue = sortInfo.sortType
				sortDirectionAsc = sortInfo.sortDirectionAsc
				excecuteSearchMovies()
			}
		"
		@editButtonClicked="
			(movie) => {
				editMovie = movie
			}
		"
		@deleteButtonClicked="
			(movie) => {
				deleteMovie = movie
			}
		"
		v-if="!firstLoad || isLoaded"
		:movies="movies"
	/>
	<PageCounter
		:page-number="pageNumber"
		:total-pages="totalPages"
		@pageChange="
			(page) => {
				pageNumber = page
			}
		"
	/>
	<ExtraListContent
		:total-pages="totalPages"
		:total-items="totalItems"
		item-type="Movies"
		@refresh="
			() => {
				excecuteSearchMovies()
			}
		"
	/>
</template>
