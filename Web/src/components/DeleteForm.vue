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
	},
	data() {
		return {
			styles,
		}
	},
	methods: {
		handleSubmit(event) {
			event.preventDefault()
			this.$emit("form-submitted")
		},
	},
}
</script>

<template>
	<form :class="styles.verticalForm" @submit="handleSubmit">
		<p>Are you sure you would like to delete this {{ actionType }}?</p>
		<input class="btn" type="submit" value="Delete" :disabled="isSubmitting" style="color: white; background-color: red" />
		<input @click="$emit('onClose')" class="btn" type="button" value="Cancel" :disabled="isSubmitting" style="border-color: black" />
	</form>
</template>
