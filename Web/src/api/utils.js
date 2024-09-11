/**
 *
 * @param {string} url
 * @param {RequestInit=} options
 * @returns
 */
export async function fetchAndValidate(url, options) {
	const response = await fetch(url, options)
	await validateResponse(response)
	return response
}

/**
 * @param {Response} response
 * @returns {Promise<void>}
 */
export async function validateResponse(response) {
	if (response.ok) {
		return
	}

	if (response.status === 400) {
		const json = await response.json()
		const errors = Object.values(json.errors)
			.flatMap((x) => x)
			.map((errStr) => ` - ${errStr}`)
		throw new Error("Invalid data:\n" + errors.join("\n"))
	}
	throw new Error(response.statusText)
}
