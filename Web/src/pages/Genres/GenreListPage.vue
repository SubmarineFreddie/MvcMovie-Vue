<script>
import { searchGenres } from "@/api"
import ExtraListContent from "@/components/ExtraListContent.vue"
import LoadingOverlay from "@/components/LoadingOverlay.vue"
import PageCounter from "@/components/PageCounter.vue"
import PageSizeSelector from "@/components/PageSizeSelector.vue"
import { debounce, delay } from "@/utils"
import CreateGenreModal from "./CreateGenreModal.vue"
import DeleteGenreModal from "./DeleteGenreModal.vue"
import EditGenreModal from "./EditGenreModal.vue"
import GenresTable from "./GenresTable.vue"

const DEBOUNCE_TIME = 500

export default {
	components: {
		LoadingOverlay,
		PageSizeSelector,
		GenresTable,
		CreateGenreModal,
		EditGenreModal,
		DeleteGenreModal,
		PageCounter,
		ExtraListContent,
	},
	data() {
		return {
			genres: [],
			pageSize: Number.parseInt(localStorage.getItem("genrePageSize")) ?? 5,
			pageNumber: 1,
			totalPages: 0,
			totalItems: 0,
			isLoaded: false,
			firstLoad: true,
			createModalOpen: false,
			editGenre: null,
			deleteGenre: null,
			sortDirectionAsc: true,
			genreSearch: "",
			debouncedSearch: debounce(this.excecuteSearchGenres, DEBOUNCE_TIME),
		}
	},
	async mounted() {
		this.excecuteSearchGenres()
	},
	methods: {
		async excecuteSearchGenres() {
			localStorage.setItem("genrePageSize", this.pageSize)
			this.isLoaded = false
			try {
				await delay(750)
				let response = await searchGenres({
					page: this.pageNumber,
					pageSize: this.pageSize,
					name: this.genreSearch || null,
					sortDirection: this.sortDirectionAsc ? 0 : 1,
				})
				this.genres = response.genres
				this.totalPages = response.pageCount
				this.totalItems = response.genreCount
				this.firstLoad = false
			} catch (error) {
				alert(error)
			} finally {
				this.isLoaded = true
			}
		},
	},
	watch: {
		genreSearch(newGenreSearch) {
			this.debouncedSearch(newGenreSearch)
		},
		pageNumber() {
			this.excecuteSearchGenres()
		},
	},
}
</script>

<template>
	<h1>Genre List</h1>
	<p>
		<input @click="createModalOpen = true" class="btn btn-primary" type="submit" value="Create New" />
	</p>
	<form style="display: flex; gap: 1rem" onsubmit="event.preventDefault();">
		<PageSizeSelector
			:page-size="pageSize"
			@sizeChange="
				(newValue) => {
					pageSize = newValue
					excecuteSearchGenres()
				}
			"
		/>
		<p>Name: <input type="text" v-model="genreSearch" /></p>
	</form>
	<CreateGenreModal
		@modalCancel="
			() => {
				createModalOpen = false
			}
		"
		@genreCreated="
			() => {
				createModalOpen = false
				excecuteSearchGenres()
			}
		"
		v-if="createModalOpen"
	/>
	<EditGenreModal
		:selected-genre="editGenre"
		@modalCancel="
			() => {
				editGenre = null
			}
		"
		@movieEdited="
			() => {
				editGenre = null
				excecuteSearchGenres()
			}
		"
		v-if="editGenre !== null"
	/>
	<DeleteGenreModal
		:selected-genre="deleteGenre"
		@modalCancel="
			() => {
				deleteGenre = null
			}
		"
		@movieDeleted="
			() => {
				deleteGenre = null
				excecuteSearchGenres()
			}
		"
		v-if="deleteGenre !== null"
	/>
	<LoadingOverlay v-if="!isLoaded" />
	<GenresTable
		@sortChanged="
			(sortDirection) => {
				sortDirectionAsc = sortDirection
				excecuteSearchGenres()
			}
		"
		@editButtonClicked="
			(genre) => {
				editGenre = genre
			}
		"
		@deleteButtonClicked="
			(genre) => {
				deleteGenre = genre
			}
		"
		v-if="!firstLoad || isLoaded"
		:genres="genres"
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
		item-type="Genres"
		@refresh="
			() => {
				excecuteSearchMovies()
			}
		"
	/>
</template>
