<script>
import { defineComponent } from "vue"
import styles from "./Modal.module.scss"

export default defineComponent({
	data() {
		return { styles }
	},
	props: {
		showClose: {
			type: Boolean,
			required: true,
		},
	},
	emits: ["onClose"],
})
</script>

<template>
	<div :class="styles.modal">
		<div :class="styles.modalContent">
			<span v-if="showClose" @click="$emit('onClose')" :class="styles.close">&times;</span>
			<slot></slot>
		</div>
	</div>
</template>
