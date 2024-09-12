<script setup lang="ts">
import { createGenre } from "@/api"
import GenreForm from "@/components/GenreForm.vue"
import Modal from "@/components/Modal.vue"
import type { Genre } from "@/models"
import { reactive, ref } from "vue"

interface Emits {
	(e: "genre-created"): void
	(e: "modal-cancel"): void
}

const emit = defineEmits<Emits>()

const genreValue = reactive<Genre>({ id: 0, name: "" })
const isSubmitting = ref(false)

async function handleSubmit() {
	if (isSubmitting.value) {
		return
	}

	isSubmitting.value = true
	try {
		await createGenre(genreValue)
		emit("genre-created")
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
			action-type="Create"
			:is-submitting="isSubmitting"
			@form-submitted="
				(genre) => {
					genreValue = genre
					handleSubmit()
				}
			"
		/>
	</Modal>
</template>
