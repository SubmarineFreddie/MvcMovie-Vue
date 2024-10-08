export function formatDateInput(date: Date | string | number) {
	const jsDate = new Date(date)
	function padZeros(str: string | number, length = 2) {
		return String(str).padStart(length, "0")
	}

	return `${padZeros(jsDate.getFullYear(), 4)}-${padZeros(jsDate.getMonth())}-${jsDate.getDate()}`
}
