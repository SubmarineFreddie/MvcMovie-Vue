<script setup lang="ts">
import { deleteGenre } from "@/api"
import DeleteForm from "@/components/DeleteForm.vue"
import Modal from "@/components/Modal.vue"
import type { Genre } from "@/models"
import { reactive, ref } from "vue"

interface Props {
	selectedGenre: Genre
}

interface Emits {
	(e: "genre-deleted"): void
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
		await deleteGenre(genreValue)
		emit("genre-deleted")
	} catch (error) {
		alert(error)
	} finally {
		isSubmitting.value = false
	}
}
</script>

<template>
	<Modal :show-close="false">
		<DeleteForm action-type="genre" :is-submitting="isSubmitting" @form-submitted="handleSubmit" @onClose="emit('modal-cancel')" />
	</Modal>
</template>
