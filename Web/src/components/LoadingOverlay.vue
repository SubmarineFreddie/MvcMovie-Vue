<script>
import loadingSpinnerGif from "@/assets/loading-7528_256.gif"
import styles from "./LoadingOverlay.module.scss"

export default {
	data() {
		return {
			loadingSpinnerGif,
			styles,
		}
	},
}
</script>

<template>
	<div :class="styles.loadingDiv">
		<img :class="styles.loadingSpinner" :src="loadingSpinnerGif" alt="Loading" />
	</div>
</template>
