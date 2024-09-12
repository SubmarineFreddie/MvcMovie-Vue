<script setup lang="ts">
import { getGenres } from "@/api"
import type { Genre, Movie } from "@/models"
import { onMounted, reactive, ref } from "vue"
import styles from "./VerticalForm.module.scss"

interface Props {
	actionType: string
	isSubmitting: boolean
	selectedMovie?: Movie
}

interface Emits {
	(e: "form-submitted", movie: Movie): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const genres = ref<Genre[]>([])
const movieValue = reactive<Movie>(
	props.selectedMovie
		? { ...props.selectedMovie }
		: { id: 0, title: "", releaseDate: "", genres: [], price: 0, rating: "", imageUrl: "", summary: "" },
)

function handleSubmit(event: Event) {
	event.preventDefault()
	emit("form-submitted", movieValue)
}

async function excecuteGetGenres() {
	try {
		let response = await getGenres()
		genres.value = response
	} catch (error) {
		alert(error)
	}
}

onMounted(excecuteGetGenres)
</script>

<template>
	<form :class="styles.verticalForm" @submit="handleSubmit">
		<label>
			Title*:
			<input v-model="movieValue.title" type="text" required />
		</label>
		<label>
			Release Date*:
			<input v-model="movieValue.releaseDate" type="date" required />
		</label>
		<label>
			Genre*:
			<select v-model="movieValue.genres" required multiple>
				<option :value="null">Please Select</option>
				<option v-for="genre in genres" :key="genre.id" :value="genre">{{ genre.name }}</option>
			</select>
		</label>
		<label>
			Price*:
			<input v-model="movieValue.price" type="number" step="any" required />
		</label>
		<label>
			Rating*:
			<input v-model="movieValue.rating" type="text" required />
		</label>
		<label>
			Image URL*:
			<input v-model="movieValue.imageUrl" type="text" required />
		</label>
		<label>
			Summary*:
			<input v-model="movieValue.summary" type="text" required />
		</label>
		<input class="btn btn-primary" type="submit" :value="actionType" :disabled="isSubmitting" />
	</form>
</template>
