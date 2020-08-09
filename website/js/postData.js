function postData(result, url) {
  var dataStatus = false;
  await fetch(url, {
    method: 'POST',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': url
    },
    body: result,
  })
    .then(response => {
      console.log('Success:', response);
      dataStatus = true;
    })
    .catch((error) => {
      console.error('Error:', error);
    });
  return dataStatus;
}
