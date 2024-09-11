<script>
import { updateGenre } from "@/api"
import GenreForm from "@/components/GenreForm.vue"
import Modal from "@/components/Modal.vue"

export default {
	props: {
		selectedGenre: {
			type: Object,
			required: true,
		},
	},
	components: {
		Modal,
		GenreForm,
	},
	data(props) {
		return {
			genreValue: {
				...props.selectedGenre,
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
				await updateGenre(this.genreValue)
				this.$emit("genreEdited")
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
