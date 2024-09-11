export function getOrCreateFromLocalStorage(key, defaultValue) {
	const value = localStorage.getItem(key)
	if (value !== null) {
		return value
	}
	localStorage.setItem(key, defaultValue)
	return defaultValue
}
