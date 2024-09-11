<script>
import { deleteGenre } from "@/api"
import DeleteForm from "@/components/DeleteForm.vue"
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
		DeleteForm,
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
				await deleteGenre(this.genreValue)
				this.$emit("genreDeleted")
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
	<Modal :show-close="false">
		<DeleteForm action-type="genre" :is-submitting="isSubmitting" @form-submitted="handleSubmit" @onClose="$emit('modalCancel')" />
	</Modal>
</template>
