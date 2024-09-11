import { fetchAndValidate } from "./utils"

export async function getGenres() {
	const response = await fetchAndValidate("/api/genres")
	return await response.json()
}

export async function getGenre(id) {
	const response = await fetchAndValidate(`/api/genres/${id}`)
	return await response.json()
}

export async function createGenre(body) {
	const response = await fetchAndValidate("/api/genres", {
		method: "POST",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}

export async function updateGenre(body) {
	const response = await fetchAndValidate("/api/genres", {
		method: "PUT",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}

export async function deleteGenre(body) {
	const response = await fetchAndValidate("/api/genres", {
		method: "DELETE",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}

export async function searchGenres(body) {
	const response = await fetchAndValidate("/api/genres/search", {
		method: "POST",
		body: JSON.stringify(body),
		headers: {
			"Content-Type": "application/json",
		},
	})
	return await response.json()
}
