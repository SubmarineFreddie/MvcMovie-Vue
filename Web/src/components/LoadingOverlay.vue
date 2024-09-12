<script setup lang="ts">
import loadingSpinnerGif from "@/assets/loading-7528_256.gif"
import styles from "./LoadingOverlay.module.scss"
</script>

<template>
	<div :class="styles.loadingDiv">
		<img :class="styles.loadingSpinner" :src="loadingSpinnerGif" alt="Loading" />
	</div>
</template>
