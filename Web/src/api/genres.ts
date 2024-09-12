import type { Genre, GenrePageInfo, GenreSearchOptions } from "@/models"
import { fetchAndValidate } from "./utils"

export async function getGenres(): Promise<Genre[]> {
	const response = await fetchAndValidate("/api/genres")
	return await response.json()
}

export async function getGenre(id: number): Promise<Genre> {
	const response = await fetchAndValidate(`/api/genres/${id}`)
	return await response.json()
}

export async function createGenre(body: Genre): Promise<Genre> {
	const response = await fetchAndValidate("/api/genres", {
		method: "POST",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}

export async function updateGenre(body: Genre): Promise<Genre> {
	const response = await fetchAndValidate("/api/genres", {
		method: "PUT",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}

export async function deleteGenre(body: Genre): Promise<Genre> {
	const response = await fetchAndValidate("/api/genres", {
		method: "DELETE",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}

export async function searchGenres(body: GenreSearchOptions = {}): Promise<GenrePageInfo> {
	const response = await fetchAndValidate("/api/genres/search", {
		method: "POST",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}
