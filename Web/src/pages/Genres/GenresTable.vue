<script setup lang="ts">
import type { Genre } from "@/models"
import { ref } from "vue"
import styles from "./GenresTable.module.scss"

interface Props {
	genres: Genre[]
}

interface Emits {
	(e: "sort-changed", isSortDirectionAsc: boolean): void
	(e: "edit-button-clicked", genre: Genre): void
	(e: "delete-button-clicked", genre: Genre): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const sortDirectionAsc = ref(true)

function setSort() {
	if (sortDirectionAsc.value) {
		sortDirectionAsc.value = false
	} else {
		sortDirectionAsc.value = true
	}
	emit("sort-changed", sortDirectionAsc.value)
}

function editButtonEmit(genre: Genre) {
	emit("edit-button-clicked", genre)
}

function deleteButtonEmit(genre: Genre) {
	emit("delete-button-clicked", genre)
}
</script>

<template>
	<table class="table">
		<thead>
			<tr>
				<th>
					<div :class="styles.tableHeader">
						<a @click="setSort()">Name</a>
						<p v-if="sortDirectionAsc">&#9660;</p>
						<p v-if="!sortDirectionAsc">&#9650;</p>
					</div>
				</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<tr v-for="genre in props.genres" :key="genre.id">
				<td>{{ genre.name }}</td>
				<td>
					<a @click="editButtonEmit(genre)">Edit</a> |
					<a @click="deleteButtonEmit(genre)">Delete</a>
				</td>
			</tr>
		</tbody>
	</table>
</template>
