<script setup lang="ts">
import styles from "./ExtraListContent.module.scss"

interface Props {
	totalPages: number
	totalItems: number
	itemType: string
}

interface Emits {
	(e: "refresh"): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

function handleRefresh(event: Event) {
	event.preventDefault()
	emit("refresh")
}
</script>

<template>
	<div :class="styles.extraContent">
		<input @click="handleRefresh" class="btn btn-primary" type="submit" value="Refresh" />
		<div :class="styles.metrics">
			<p>Total Pages: {{ props.totalPages }}</p>
			<p>Total {{ props.itemType }}: {{ props.totalItems }}</p>
		</div>
	</div>
</template>
