<script setup lang="ts">
import { searchGenres } from "@/api"
import ExtraListContent from "@/components/ExtraListContent.vue"
import LoadingOverlay from "@/components/LoadingOverlay.vue"
import PageCounter from "@/components/PageCounter.vue"
import PageSizeSelector from "@/components/PageSizeSelector.vue"
import { useLocalStorage } from "@/composition"
import { SortDirection, type Genre } from "@/models"
import { delay } from "@/utils"
import { useDebounceFn } from "@vueuse/core"
import { onMounted, ref, watch } from "vue"
import CreateGenreModal from "./CreateGenreModal.vue"
import DeleteGenreModal from "./DeleteGenreModal.vue"
import EditGenreModal from "./EditGenreModal.vue"
import GenresTable from "./GenresTable.vue"

const genres = ref<Genre[]>([])
const pageSize = useLocalStorage("genrePageSize", 5)
const pageNumber = ref(1)
const totalPages = ref(0)
const totalItems = ref(0)
const isLoaded = ref(false)
const firstLoad = ref(true)
const createModalOpen = ref(false)
const editGenre = ref<Genre | null>(null)
const deleteGenre = ref<Genre | null>(null)
const sortDirectionAsc = ref(true)
const nameSearch = ref("")

const debouncedSearch = useDebounceFn(excecuteSearchGenres, 500)

async function excecuteSearchGenres() {
	isLoaded.value = false
	try {
		await delay(250)
		let response = await searchGenres({
			page: pageNumber.value,
			pageSize: pageSize.value,
			name: nameSearch.value || null,
			sortDirection: sortDirectionAsc.value ? SortDirection.Ascending : SortDirection.Descending,
		})
		genres.value = response.genres
		totalPages.value = response.pageCount
		totalItems.value = response.genreCount
		firstLoad.value = false
	} catch (error) {
		alert(error)
	} finally {
		isLoaded.value = true
	}
}

watch(nameSearch, debouncedSearch)
watch(pageNumber, excecuteSearchGenres)

onMounted(excecuteSearchGenres)
</script>

<template>
	<h1>Genre List</h1>
	<p>
		<input @click="createModalOpen = true" class="btn btn-primary" type="submit" value="Create New" />
	</p>
	<form style="display: flex; gap: 1rem" @submit="(event) => event.preventDefault()">
		<PageSizeSelector
			:page-size="pageSize"
			@size-change="
				(newValue) => {
					pageSize = newValue
					excecuteSearchGenres()
				}
			"
		/>
		<p>Name: <input type="text" v-model="nameSearch" placeholder="e.g. Comedy" /></p>
	</form>
	<CreateGenreModal
		v-if="createModalOpen"
		@modalCancel="() => (createModalOpen = false)"
		@genreCreated="
			() => {
				createModalOpen = false
				excecuteSearchGenres()
			}
		"
	/>
	<EditGenreModal
		v-if="editGenre !== null"
		:selected-genre="editGenre"
		@modalCancel="() => (editGenre = null)"
		@movieEdited="
			() => {
				editGenre = null
				excecuteSearchGenres()
			}
		"
	/>
	<DeleteGenreModal
		v-if="deleteGenre !== null"
		:selected-genre="deleteGenre"
		@modalCancel="() => (deleteGenre = null)"
		@movieDeleted="
			() => {
				deleteGenre = null
				excecuteSearchGenres()
			}
		"
	/>
	<LoadingOverlay v-if="!isLoaded" />
	<GenresTable
		v-if="!firstLoad || isLoaded"
		@sort-changed="
			(sortDirection) => {
				sortDirectionAsc = sortDirection
				excecuteSearchGenres()
			}
		"
		@edit-button-clicked="(genre) => (editGenre = genre)"
		@delete-button-clicked="(genre) => (deleteGenre = genre)"
		:genres="genres"
	/>
	<PageCounter :page-number="pageNumber" :total-pages="totalPages" @page-change="(page) => (pageNumber = page)" />
	<ExtraListContent :total-pages="totalPages" :total-items="totalItems" item-type="Genres" @refresh="excecuteSearchGenres" />
</template>
