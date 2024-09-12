<script setup lang="ts">
import styles from "./VerticalForm.module.scss"

interface Props {
	actionType: string
	isSubmitting: boolean
}

interface Emits {
	(e: "form-submitted"): void
	(e: "on-close"): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

function handleSubmit(event: Event) {
	event.preventDefault()
	emit("form-submitted")
}
</script>

<template>
	<form :class="styles.verticalForm" @submit="handleSubmit">
		<p>Are you sure you would like to delete this {{ props.actionType }}?</p>
		<input class="btn" type="submit" value="Delete" :disabled="props.isSubmitting" style="color: white; background-color: red" />
		<input @click="emit('on-close')" class="btn" type="button" value="Cancel" :disabled="props.isSubmitting" style="border-color: black" />
	</form>
</template>
