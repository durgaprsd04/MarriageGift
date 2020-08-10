class ServerInteraction
{
  static async function postData(result, url) {
    console.log('POSTing data to '+url);
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
  static async function getData(getIdUrl)
  {
    result={};
    console.log("Fetching data from url "+getIdUrl);
    await  fetch(getIdUrl)
      .then(response => response.json())
      .then(data =>{
          console.log(data);
          result["result"]=data;
      });
      return result;
  }
}
class HTMLSnippets {
  function GetSelectList(selectListId, isMultiple, maxNumberOfSelects, optionDict)
  {
    var initSelect = `<select id="`+selectListId+`" name="`+selectListId+`" size="`;
    if(isMultiple)
      initSelect = initSelect + maxNumberOfSelects+`" multiple required> {optionList} </select>`;
    else
      initSelect = initSelect + `1" required> {optionList} </select>`;
    var resultText="";
    var part1='<option value="';
    var part2='">';
    var part3='</option>';
    for (var key in optionDict)
    {
        resultText += part1+key+part2+json[key]+part3;
    }
    initSelect = initSelect.replace("{optionlist}", resultText);
    return initSelect;
  }
}
