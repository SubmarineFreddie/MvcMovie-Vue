<script>
import { getGenres } from "@/api"
import styles from "./VerticalForm.module.scss"

export default {
	props: {
		actionType: {
			type: String,
			required: true,
		},
		isSubmitting: {
			type: Boolean,
			required: true,
		},
		selectedMovie: {
			type: Object,
			required: false,
		},
	},
	data(props) {
		return {
			styles,
			genres: [],
			movieValue: props.selectedMovie
				? { ...props.selectedMovie }
				: {
						title: "",
						releaseDate: "",
						genres: [],
						price: 0,
						rating: "",
						imageUrl: "",
						summary: "",
					},
		}
	},
	async mounted() {
		this.excecuteGetGenres()
	},
	methods: {
		handleSubmit(event) {
			event.preventDefault()
			this.$emit("form-submitted", this.movieValue)
		},
		async excecuteGetGenres() {
			try {
				let response = await getGenres()
				this.genres = response
			} catch (error) {
				alert(error)
			}
		},
	},
}
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
				<option v-for="genre in genres" :value="genre">{{ genre.name }}</option>
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
