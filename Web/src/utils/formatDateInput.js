export function formatDateInput(date) {
	const jsDate = new Date(date)
	function padZeros(str, length = 2) {
		return String(str).padStart(length, "0")
	}

	return `${padZeros(jsDate.getFullYear(), 4)}-${padZeros(jsDate.getMonth())}-${jsDate.getDate()}`
}
