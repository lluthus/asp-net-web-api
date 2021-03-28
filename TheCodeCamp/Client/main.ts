

function init() {
	const input: HTMLInputElement = document.querySelector('.input');
	const output: HTMLInputElement = document.querySelector('.output');
	const close: HTMLInputElement = document.querySelector('.close');
	const channel = Math.random();

	//const supportsRequestStreams = !new Request('', {
	//	body: new ReadableStream(),
	//	method: 'POST',
	//}).headers.has('Content-Type');

	//if (!supportsRequestStreams) {
	//	output.textContent = `It doesn't look like your browser supports request streams.`;
	//	return;
	//}

	//const stream = new ReadableStream({
	//	start(controller) {
	//		input.addEventListener('input', (event) => {
	//			event.preventDefault();
	//			controller.enqueue(input.value);
	//			input.value = '';
	//		});

	//		close.addEventListener('click', () => controller.close());
	//	}
	//}).pipeThrough(new TextEncoderStream());

	//fetch(`http://aspnetwebapi101.azurewebsites.net/api/Stream?channel=${channel}`, {
	//	method: 'GET',
	//	headers: { 'accept': 'application/json' },
	//	body: stream//,
	//	//allowHTTP1ForStreamingUpload: true,
	//});

	fetch('http://aspnetwebapi101.azurewebsites.net/api/Stream', { 
		headers: {
			'accept': 'application/json'
		}
	}).then(async res => {
		const reader = res.body.pipeThrough(new TextDecoderStream()).getReader();
		while (true) {
			const { done, value } = await reader.read();
			if (done) return;
			output.append(value);
		}
	});
}

init();