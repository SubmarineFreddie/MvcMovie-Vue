<script setup lang="ts">
import styles from "./Modal.module.scss"

interface Props {
	showClose: boolean
}

interface Emits {
	(e: "on-close"): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()
</script>

<template>
	<div :class="styles.modal">
		<div :class="styles.modalContent">
			<span v-if="props.showClose" @click="emit('on-close')" :class="styles.close">&times;</span>
			<slot></slot>
		</div>
	</div>
</template>
