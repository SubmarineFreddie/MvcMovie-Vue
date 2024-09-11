<script>
import { updateMovie } from "@/api"
import Modal from "@/components/Modal.vue"
import MovieForm from "@/components/MovieForm.vue"

export default {
	props: {
		selectedMovie: {
			type: Object,
			required: true,
		},
	},
	components: {
		Modal,
		MovieForm,
	},
	data(props) {
		return {
			movieValue: {
				...props.selectedMovie,
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
				await updateMovie(this.movieValue)
				this.$emit("movieEdited")
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
