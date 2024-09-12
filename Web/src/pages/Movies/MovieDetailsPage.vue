<script setup lang="ts">
import { deleteMovie, getMovie, updateMovie } from "@/api"
import DeleteForm from "@/components/DeleteForm.vue"
import LoadingOverlay from "@/components/LoadingOverlay.vue"
import Modal from "@/components/Modal.vue"
import MovieForm from "@/components/MovieForm.vue"
import { useQueryId } from "@/composition/useQueryId"
import type { Movie } from "@/models"
import { delay } from "@/utils"
import { computed, onMounted, ref, watch } from "vue"
import { useRouter } from "vue-router"
import styles from "./MovieDetailsPage.module.scss"

const router = useRouter()
const movieId = useQueryId("id")
const movieValue = ref<Movie | null>(null)
const isLoaded = ref(false)
const firstLoad = ref(true)
const isSubmitting = ref(false)
const showSavedMessage = ref(false)
const deleteModalOpen = ref(false)

const movieImage = computed(() =>
	movieValue.value != null && movieValue.value.imageUrl != null ? movieValue.value.imageUrl : "src/assets/NoImagePlaceholder.svg",
)
const movieSummary = computed(() =>
	movieValue.value != null && movieValue.value.summary != null ? movieValue.value.summary : "No Summary Found",
)

function redirectToMovieList() {
	router.push("/movielist")
}

async function loadMovie(id: number) {
	isLoaded.value = false
	try {
		await delay(750)
		movieValue.value = await getMovie(id)
		firstLoad.value = false
		isSubmitting.value = false
		showSavedMessage.value = false
		deleteModalOpen.value = false
	} catch (error) {
		alert(error)
	} finally {
		isLoaded.value = true
	}
}

async function handleUpdate() {
	if (isSubmitting.value || movieValue.value == null) {
		return
	}

	isSubmitting.value = true
	try {
		await updateMovie(movieValue.value)
		showSavedMessage.value = true
	} catch (error) {
		alert(error)
	} finally {
		isSubmitting.value = false
	}
}

async function handleDelete() {
	if (isSubmitting.value || movieValue.value == null) {
		return
	}

	isSubmitting.value = true
	try {
		await deleteMovie(movieValue.value)
		redirectToMovieList()
	} catch (error) {
		alert(error)
	} finally {
		isSubmitting.value = false
	}
}

watch(movieId, () => loadMovie(movieId.value))
onMounted(() => loadMovie(movieId.value))
</script>

<template>
	<h1>Movie</h1>
	<p>
		<input @click="redirectToMovieList" class="btn btn-primary" type="submit" value="Back To Movie List" />
	</p>
	<LoadingOverlay v-if="!isLoaded" />
	<div v-if="movieValue" :class="styles.splitBox">
		<div :class="styles.detailBox">
			<MovieForm
				action-type="Save"
				:is-submitting="isSubmitting"
				:selected-movie="movieValue"
				@form-submitted="
					(movie) => {
						movieValue = movie
						handleUpdate()
					}
				"
			/>
			<h3 v-if="showSavedMessage" style="color: green">Movie Details Saved!</h3>
			<input
				@click="deleteModalOpen = true"
				class="btn"
				type="button"
				value="Delete"
				:disabled="isSubmitting"
				style="color: white; background-color: red; width: 100%"
			/>
		</div>
		<div :class="styles.infoBox">
			<img :src="movieImage" />
			<h3>Summary:</h3>
			<p>{{ movieSummary }}</p>
		</div>
	</div>
	<Modal v-if="deleteModalOpen" :show-close="false">
		<DeleteForm @onClose="deleteModalOpen = false" action-type="movie" :is-submitting="isSubmitting" @form-submitted="handleDelete" />
	</Modal>
</template>
