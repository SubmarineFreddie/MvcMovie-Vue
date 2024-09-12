<script setup lang="ts">
import type { Genre } from "@/models"
import { reactive } from "vue"
import styles from "./VerticalForm.module.scss"

interface Props {
	actionType: string
	isSubmitting: boolean
	selectedGenre?: Genre
}

interface Emits {
	(e: "form-submitted", genre: Genre): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const genreValue = reactive<Genre>(props.selectedGenre ? { ...props.selectedGenre } : { id: 0, name: "" })

function handleSubmit(event: Event) {
	event.preventDefault()
	emit("form-submitted", genreValue)
}
</script>

<template>
	<form :class="styles.verticalForm" @submit="handleSubmit">
		<label>
			Name*:
			<input v-model="genreValue.name" type="text" required />
		</label>
		<input class="btn btn-primary" type="submit" :value="actionType" :disabled="isSubmitting" />
	</form>
</template>
