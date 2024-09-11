<script>
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
		selectedGenre: {
			type: Object,
			required: false,
		},
	},
	data(props) {
		return {
			styles,
			formType: props.actionType,
			genreValue: props.selectedGenre
				? { ...props.selectedGenre }
				: {
						name: "",
					},
		}
	},
	methods: {
		handleSubmit(event) {
			event.preventDefault()
			this.$emit("form-submitted", this.genreValue)
		},
	},
}
</script>

<template>
	<form :class="styles.verticalForm" @submit="handleSubmit">
		<label>
			Name*:
			<input v-model="genreValue.name" type="text" required />
		</label>
		<input class="btn btn-primary" type="submit" :value="formType" :disabled="isSubmitting" />
	</form>
</template>
