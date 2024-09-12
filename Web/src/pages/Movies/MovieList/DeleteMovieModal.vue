<script setup lang="ts">
import { deleteMovie } from "@/api"
import DeleteForm from "@/components/DeleteForm.vue"
import Modal from "@/components/Modal.vue"
import type { Movie } from "@/models"
import { reactive, ref } from "vue"

interface Props {
	selectedMovie: Movie
}

interface Emits {
	(e: "movie-deleted"): void
	(e: "modal-cancel"): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const movieValue = reactive<Movie>(props.selectedMovie)
const isSubmitting = ref(false)

async function handleSubmit() {
	if (isSubmitting.value) {
		return
	}

	isSubmitting.value = true
	try {
		await deleteMovie(movieValue)
		emit("movie-deleted")
	} catch (error) {
		alert(error)
	} finally {
		isSubmitting.value = false
	}
}
</script>

<template>
	<Modal :show-close="false">
		<DeleteForm action-type="movie" :is-submitting="isSubmitting" @form-submitted="handleSubmit" @onClose="$emit('modal-cancel')" />
	</Modal>
</template>
