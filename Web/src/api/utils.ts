export async function fetchAndValidate(input: RequestInfo | URL, init?: RequestInit) {
	const response = await fetch(input, init)
	await validateResponse(response)
	return response
}

export async function validateResponse(response: Response) {
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
