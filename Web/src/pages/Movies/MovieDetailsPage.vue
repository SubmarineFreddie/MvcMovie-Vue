<script>
import { deleteMovie, getMovie, updateMovie } from "@/api"
import DeleteForm from "@/components/DeleteForm.vue"
import LoadingOverlay from "@/components/LoadingOverlay.vue"
import Modal from "@/components/Modal.vue"
import MovieForm from "@/components/MovieForm.vue"
import { delay } from "@/utils"
import { useRoute } from "vue-router"
import styles from "./MovieDetailsPage.module.scss"

export default {
	components: {
		Modal,
		MovieForm,
		DeleteForm,
		LoadingOverlay,
	},
	data() {
		return {
			styles,
			movieId: useRoute().query.id,
			movieValue: null,
			isLoaded: false,
			firstLoad: true,
			isSubmitting: false,
			showSavedMessage: false,
			deleteModalOpen: false,
		}
	},
	computed: {
		movieImage() {
			return this.movieValue.imageUrl == null ? "src/assets/NoImagePlaceholder.svg" : this.movieValue.imageUrl
		},
		movieSummary() {
			return this.movieValue.summary?.length ? this.movieValue.summary : "No Summary Found"
		},
	},
	async mounted() {
		this.loadMovie(this.movieId)
	},
	methods: {
		async loadMovie(id) {
			this.isLoaded = false
			try {
				await delay(750)
				this.movieValue = await getMovie(id)
				this.firstLoad = false
			} catch (error) {
				alert(error)
			} finally {
				this.isLoaded = true
			}
		},
		async handleUpdate() {
			if (this.isSubmitting) {
				return
			}

			this.isSubmitting = true
			try {
				await updateMovie(this.movieValue)
				this.showSavedMessage = true
			} catch (error) {
				alert(error)
			} finally {
				this.isSubmitting = false
			}
		},
		async handleDelete() {
			if (this.isSubmitting) {
				return
			}

			this.isSubmitting = true
			try {
				await deleteMovie(this.movieValue)
				this.redirectToMovieList()
			} catch (error) {
				alert(error)
			} finally {
				this.isSubmitting = false
			}
		},
		redirectToMovieList() {
			this.$router.push("/movielist")
		},
	},
}
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
