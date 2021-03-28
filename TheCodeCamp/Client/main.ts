

function init() {


	fetch('http://aspnetwebapi101.azurewebsites.net/api/Stream', { 
		headers: {
			'accept': 'application/json'
		}
	}).then(async res => {
		const reader = res.body?.pipeThrough(new TextDecoderStream()).getReader();
		while (true && reader) {
			const { done, value } = await reader.read();
			if (done) return;
			var output = document.querySelector('.output');
			if (output && value) {
				output.append(value);
			}
		}
	});
}

init();