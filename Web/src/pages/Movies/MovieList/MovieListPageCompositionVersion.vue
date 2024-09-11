<script setup>
import { getMovies, searchMovies } from "@/api"
import LoadingOverlay from "@/components/LoadingOverlay.vue"
import { delay } from "@/utils"
import { watchDebounced } from "@vueuse/core"
import { onMounted, reactive, ref } from "vue"
import MoviesTable from "./MoviesTable.vue"

const data = reactive({
	movies: [],
	isLoaded: false,
	firstLoad: true,
})

const searchContent = ref("")
async function excecuteSearchMovies(criteria) {
	data.isLoaded = false
	try {
		await delay(750)
		data.movies = await searchMovies({
			title: criteria,
			genre: null,
			releasedAfter: null,
		})
		data.firstLoad = false
	} catch (error) {
		alert(error)
	} finally {
		data.isLoaded = true
	}
}

watchDebounced(searchContent, (newSearch) => excecuteSearchMovies(newSearch), {
	debounce: 500,
})

async function loadMovies() {
	data.isLoaded = false
	try {
		await delay(750)
		data.movies = await getMovies()
		data.firstLoad = false
	} catch (error) {
		alert(error)
	} finally {
		data.isLoaded = true
	}
}

onMounted(loadMovies)
</script>

<template>
	<h1>Movie List</h1>
	<p>
		<a>Create New</a>
	</p>
	<form onsubmit="event.preventDefault();">
		<p>
			<select>
				<option value="">All</option>
			</select>
			Title: <input type="text" v-model="searchContent" />
			<input class="btn btn-primary" type="submit" value="Filter" />
		</p>
	</form>

	<LoadingOverlay v-if="!data.isLoaded" />
	<MoviesTable v-if="!data.firstLoad || data.isLoaded" :movies="data.movies" />
	<input @click="loadMovies()" class="btn btn-primary" type="submit" value="Refresh" />
</template>
