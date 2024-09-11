<script>
import { createMovie } from "@/api"
import Modal from "@/components/Modal.vue"
import MovieForm from "@/components/MovieForm.vue"

export default {
	components: {
		Modal,
		MovieForm,
	},
	data() {
		return {
			movieValue: {
				title: "",
				releaseDate: "",
				genre: "",
				price: 0,
				rating: "",
			},
			isSubmitting: false,
		}
	},
	methods: {
		async handleSubmit() {
			if (this.isSubmitting) {
				return
			}

			this.isSubmitting = true
			try {
				await createMovie(this.movieValue)
				this.$emit("movieCreated")
			} catch (error) {
				alert(error)
			} finally {
				this.isSubmitting = false
			}
		},
	},
}
</script>

<template>
	<Modal show-close @onClose="$emit('modalCancel')">
		<MovieForm
			action-type="Create"
			:is-submitting="isSubmitting"
			@formSubmitted="
				(movie) => {
					movieValue = movie
					handleSubmit()
				}
			"
		/>
	</Modal>
</template>
