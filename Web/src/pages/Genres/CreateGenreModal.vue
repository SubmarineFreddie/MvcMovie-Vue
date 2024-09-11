<script>
import { createGenre } from "@/api"
import GenreForm from "@/components/GenreForm.vue"
import Modal from "@/components/Modal.vue"

export default {
	components: {
		Modal,
		GenreForm,
	},
	data() {
		return {
			genreValue: {
				name: "",
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
				await createGenre(this.genreValue)
				this.$emit("genreCreated")
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
		<GenreForm
			action-type="Create"
			:is-submitting="isSubmitting"
			@formSubmitted="
				(genre) => {
					genreValue = genre
					handleSubmit()
				}
			"
		/>
	</Modal>
</template>
