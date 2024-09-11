/**
 * @param {number} time Time in ms to delay for
 */
export function delay(time) {
	return new Promise((resolve) => setTimeout(resolve, time))
}
