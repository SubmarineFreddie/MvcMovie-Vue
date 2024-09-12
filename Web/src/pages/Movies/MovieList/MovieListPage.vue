<script setup lang="ts">
import { getGenres, searchMovies } from "@/api"
import ExtraListContent from "@/components/ExtraListContent.vue"
import LoadingOverlay from "@/components/LoadingOverlay.vue"
import PageCounter from "@/components/PageCounter.vue"
import PageSizeSelector from "@/components/PageSizeSelector.vue"
import { useLocalStorage } from "@/composition"
import { MovieSortType, SortDirection, type Genre, type Movie } from "@/models"
import { delay } from "@/utils"
import { useDebounceFn } from "@vueuse/core"
import { onMounted, ref, watch } from "vue"
import CreateMovieModal from "./CreateMovieModal.vue"
import DeleteMovieModal from "./DeleteMovieModal.vue"
import EditMovieModal from "./EditMovieModal.vue"
import MoviesTable from "./MoviesTable.vue"

const movies = ref<Movie[]>([])
const genres = ref<Genre[]>([])
const isLoaded = ref(false)
const firstLoad = ref(true)
const sortValue = ref(0)
const sortDirectionAsc = ref(true)
const genreSearch = ref(null)
const titleSearch = ref("")
const debouncedSearch = useDebounceFn(excecuteSearchMovies, 500)
const releaseSearch = ref("")
const createModalOpen = ref(false)
const editMovie = ref<Movie | null>(null)
const deleteMovie = ref<Movie | null>(null)
const pageSize = useLocalStorage("genrePageSize", 5)
const pageNumber = ref(1)
const totalPages = ref(0)
const totalItems = ref(0)

// function findMovieById(id: number) {
// 	const filteredMovies = movies.value.filter((movie) => {
// 		return movie.id === id
// 	})
// 	if (filteredMovies.length === 1) {
// 		return filteredMovies[0]
// 	}
// 	if (filteredMovies.length === 0) {
// 		throw new Error("Movie not found")
// 	}
// 	throw new Error(`Multiple movies found with id ${id}`)
// }

async function excecuteSearchMovies() {
	isLoaded.value = false
	let movieSortType
	switch (sortValue.value) {
		case 0:
			movieSortType = MovieSortType.None
			break
		case 1:
			movieSortType = MovieSortType.Title
			break
		case 2:
			movieSortType = MovieSortType.ReleaseDate
			break
		case 3:
			movieSortType = MovieSortType.Genre
			break
		case 4:
			movieSortType = MovieSortType.Rating
			break
	}
	try {
		await delay(250)
		let response = await searchMovies({
			page: pageNumber.value,
			pageSize: pageSize.value,
			title: titleSearch.value || null,
			genreIds: genreSearch.value === null ? undefined : [genreSearch.value],
			releasedAfter: releaseSearch.value || undefined,
			sortType: movieSortType,
			sortDirection: sortDirectionAsc.value ? SortDirection.Ascending : SortDirection.Descending,
		})
		movies.value = response.movies
		totalPages.value = response.pageCount
		totalItems.value = response.movieCount
		firstLoad.value = false
	} catch (error) {
		alert(error)
	} finally {
		isLoaded.value = true
	}
}

async function excecuteGetGenres() {
	try {
		let response = await getGenres()
		genres.value = response
	} catch (error) {
		alert(error)
	}
}

watch(titleSearch, debouncedSearch)
watch(releaseSearch, excecuteSearchMovies)
watch(pageNumber, excecuteSearchMovies)

onMounted(excecuteGetGenres)
onMounted(excecuteSearchMovies)
</script>

<template>
	<h1>Movie List</h1>
	<p>
		<input @click="createModalOpen = true" class="btn btn-primary" type="submit" value="Create New" />
	</p>
	<form style="display: flex; gap: 1rem" onsubmit="event.preventDefault();">
		<PageSizeSelector
			:page-size="pageSize"
			@size-change="
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
				<option v-for="genre in genres" :key="genre.id" :value="genre.id">{{ genre.name }}</option>
			</select>
		</p>
		<p>Title: <input type="text" v-model="titleSearch" placeholder="e.g. Ghostbusters" /></p>
		<p>Released After: <input type="date" v-model="releaseSearch" /></p>
	</form>
	<CreateMovieModal
		@modal-cancel="() => (createModalOpen = false)"
		@movie-created="
			() => {
				createModalOpen = false
				excecuteSearchMovies()
			}
		"
		v-if="createModalOpen"
	/>
	<EditMovieModal
		:selected-movie="editMovie"
		@modal-cancel="() => (editMovie = null)"
		@movie-edited="
			() => {
				editMovie = null
				excecuteSearchMovies()
			}
		"
		v-if="editMovie !== null"
	/>
	<DeleteMovieModal
		:selected-movie="deleteMovie"
		@modal-cancel="() => (deleteMovie = null)"
		@movie-deleted="
			() => {
				deleteMovie = null
				excecuteSearchMovies()
			}
		"
		v-if="deleteMovie !== null"
	/>
	<LoadingOverlay v-if="!isLoaded" />
	<MoviesTable
		@sort-changed="
			(sortType: number, sortDirection: boolean) => {
				sortValue = sortType
				sortDirectionAsc = sortDirection
				excecuteSearchMovies()
			}
		"
		@edit-button-clicked="(movie: Movie) => (editMovie = movie)"
		@delete-button-clicked="(movie: Movie) => (deleteMovie = movie)"
		v-if="!firstLoad || isLoaded"
		:movies="movies"
	/>
	<PageCounter :page-number="pageNumber" :total-pages="totalPages" @page-change="(page) => (pageNumber = page)" />
	<ExtraListContent :total-pages="totalPages" :total-items="totalItems" item-type="Movies" @refresh="() => excecuteSearchMovies()" />
</template>
