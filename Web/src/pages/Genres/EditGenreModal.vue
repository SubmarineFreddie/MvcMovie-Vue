<script setup lang="ts">
import { updateGenre } from "@/api"
import GenreForm from "@/components/GenreForm.vue"
import Modal from "@/components/Modal.vue"
import type { Genre } from "@/models"
import { reactive, ref } from "vue"

interface Props {
	selectedGenre: Genre
}

interface Emits {
	(e: "genre-edited"): void
	(e: "modal-cancel"): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const genreValue = reactive<Genre>(props.selectedGenre)
const isSubmitting = ref(false)

async function handleSubmit() {
	if (isSubmitting.value) {
		return
	}

	isSubmitting.value = true
	try {
		await updateGenre(genreValue)
		emit("genre-edited")
	} catch (error) {
		alert(error)
	} finally {
		isSubmitting.value = false
	}
}
</script>

<template>
	<Modal show-close @onClose="$emit('modal-cancel')">
		<GenreForm
			action-type="Update"
			:is-submitting="isSubmitting"
			:selected-genre="genreValue"
			@form-submitted="
				(genre) => {
					genreValue = genre
					handleSubmit()
				}
			"
		/>
	</Modal>
</template>
