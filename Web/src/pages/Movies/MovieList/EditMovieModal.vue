<script setup lang="ts">
import { updateMovie } from "@/api"
import Modal from "@/components/Modal.vue"
import MovieForm from "@/components/MovieForm.vue"
import type { Movie } from "@/models"
import { reactive, ref } from "vue"

interface Props {
	selectedMovie: Movie
}

interface Emits {
	(e: "movie-edited"): void
	(e: "modal-cancel"): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const movieValue = reactive<Movie>({ ...props.selectedMovie })
const isSubmitting = ref(false)

async function handleSubmit() {
	if (isSubmitting.value) {
		return
	}

	isSubmitting.value = true
	try {
		await updateMovie(movieValue)
		emit("movie-edited")
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
			action-type="Update"
			:is-submitting="isSubmitting"
			:selected-movie="movieValue"
			@form-submitted="
				(movie) => {
					movieValue = movie
					handleSubmit()
				}
			"
		/>
	</Modal>
</template>
