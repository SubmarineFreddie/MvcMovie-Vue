<script setup lang="ts">
import { createMovie } from "@/api"
import Modal from "@/components/Modal.vue"
import MovieForm from "@/components/MovieForm.vue"
import type { Movie } from "@/models"
import { reactive, ref } from "vue"

interface Emits {
	(e: "movie-created"): void
	(e: "modal-cancel"): void
}

const emit = defineEmits<Emits>()

const movieValue = reactive<Movie>({
	id: 0,
	title: "",
	releaseDate: "",
	genres: [],
	price: 0,
	rating: "",
})

const isSubmitting = ref(false)

async function handleSubmit() {
	if (isSubmitting.value) {
		return
	}

	isSubmitting.value = true
	try {
		await createMovie(movieValue)
		emit("movie-created")
	} catch (error) {
		alert(error)
	} finally {
		isSubmitting.value = false
	}
}
</script>

<template>
	<Modal show-close @onClose="$emit('modal-cancel')">
		<MovieForm
			action-type="Create"
			:is-submitting="isSubmitting"
			@form-submitted="
				(movie) => {
					movieValue = movie
					handleSubmit()
				}
			"
		/>
	</Modal>
</template>
