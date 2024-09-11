<script>
import { deleteMovie } from "@/api"
import DeleteForm from "@/components/DeleteForm.vue"
import Modal from "@/components/Modal.vue"
import { defineComponent } from "vue"

export default defineComponent({
	props: {
		selectedMovie: {
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
				await deleteMovie(this.movieValue)
				this.$emit("movieDeleted")
			} catch (error) {
				alert(error)
			} finally {
				this.isSubmitting = false
			}
		},
	},
})
</script>

<template>
	<Modal :show-close="false">
		<DeleteForm action-type="movie" :is-submitting="isSubmitting" @form-submitted="handleSubmit" @onClose="$emit('modalCancel')" />
	</Modal>
</template>
