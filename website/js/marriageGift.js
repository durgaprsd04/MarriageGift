class ServerInteraction
{
  static async postData(result, url) {
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
        //console.log('Success:', response);
        dataStatus = true;
      })
      .catch((error) => {
        console.error('Error:', error);
      });
    return dataStatus;
  }
  static async getData(getIdUrl)
  {
    var result={};
    console.log('Fetching data from url '+getIdUrl);
    await  fetch(getIdUrl)
      .then(response => response.json())
      .then(data =>{
          //console.log(data);
          result['result']=data;
      });
      return result;
  }
}
class URIProvider
{
  static GetURIForOccassion(type)
  {
    if(type=="occassion")
      return "https://localhost:5001/CustomerAction/occassionTypes";
    else if (type=="allgifts")
      return "https://localhost:5001/CustomerAction/allgifts";
    else if(type=="login")
      return 'https://localhost:5001/CustomerAction/login';
    else if(type=='getCustomerByName')
      return 'https://localhost:5001/CustomerAction';
  }
  static GetParameterListForType(type)
  {
    if(type=='getCustomerByName')
    {
      return ['customerName'];
    }
  }
}

class HTMLFormatter
{
     static showOneFormAlone(formIdInQ)
     {
      var formIdList =["InviteUser","modifyEvent","BuyGiftForInvite", "RemoveGiftForInvite","RespondToInvite","ChangePassword","createEvent","loginScreenDiv"];
      for(var formId in formIdList)
      {
          if(formIdInQ==formIdList[formId])
            continue;
          document.getElementById(formIdList[formId]).setAttribute("style", "display:none");
      }
      if(formIdInQ=="none")
        return;
      document.getElementById(formIdInQ).setAttribute("style", "display:block");
    }
    static makeUserActionVisible(username)
    {
      HTMLFormatter.showOneFormAlone("none");
      document.getElementById("UserActionMenu").setAttribute("style","display:inline");
      document.getElementById("UserActionMenu").innerHTML=  username +"'s Action(s)";
      document.getElementById("CustomerActionMenu").classList.remove('disabled');
    }
  }

class HTMLSnippets
{
  GetSelectList(selectListId, isMultiple, maxNumberOfSelects, optionDict)
  {
    var initSelect = `<select id='`+selectListId+`' name='`+selectListId+`' size='`;
    if(isMultiple)
      initSelect = initSelect + maxNumberOfSelects+`' multiple required> {optionList} </select>`;
    else
      initSelect = initSelect + `1' required> {optionList} </select>`;
    var resultText='';
    var part1=`<option value='`;
    var part2=`'>`;
    var part3=`</option>`;
    for (var key in optionDict)
    {
        resultText += part1+key+part2+optionDict[key]+part3;
    }
    //console.log(resultText);
    initSelect = initSelect.replace('{optionList}', resultText);
    return initSelect;
  }
}

class LoginActions
{
  async SendDataForCustomerLogin(result)
  {
      var dataStatus =await ServerInteraction.postData(result, URIProvider.GetURIForOccassion("login"));
      return dataStatus;
  }
  async GetCustomerIdByName(customerName)
  {
    //console.log(customerName);
    var urlForCustomerDetails = URIProvider.GetURIForOccassion("getCustomerByName")+'/';
    var parameterList="";
    var paramArray = URIProvider.GetParameterListForType('getCustomerByName');
    //console.log(paramArray);
    for(var param in paramArray)
    {
      //console.log(param);
      parameterList = parameterList + paramArray[param]+'='+customerName[paramArray[param]];
    }
    urlForCustomerDetails = urlForCustomerDetails + parameterList;
    var result = await ServerInteraction.getData(urlForCustomerDetails);
    return result;
  }
}
class DataFormatter
{
  FormatJSONToDict(json)
  {
    return JSON.parse(json);
  }
}
async function createEvent1()
{
  var divElementOccassionPreText="createEventOccasionList";
  var divElementEventPreText="createEventExpectedGiftList";
  var dataFormatter = new DataFormatter();
  var htmlSnippets = new HTMLSnippets();
  var uriProvider = new URIProvider();
  //populating occasions
  var occassionDict =await ServerInteraction.getData(URIProvider.GetURIForOccassion("occassion"));
  document.getElementById(divElementOccassionPreText+"Div").innerHTML=htmlSnippets.GetSelectList(divElementOccassionPreText+"Select", false, 3,occassionDict['result']);
  //reading  gifts
  var giftDict = await ServerInteraction.getData(URIProvider.GetURIForOccassion("allgifts"));
  document.getElementById(divElementEventPreText+"Div").innerHTML=htmlSnippets.GetSelectList(divElementEventPreText+"Select", true, 3,giftDict['result']);
  //show just create event
 HTMLFormatter.showOneFormAlone("createEvent");
}
async function loginScreenValidateForm()
{
  var username=document.getElementById("loginScreenUsername").value;
  var password=document.getElementById("loginScreenPassword").value;
  result={};
  paramList={};
  result["id"]="";
  result["username"]=username;
  result["password"]=password;
  paramList["customerName"]= username;
  var loginActions = new LoginActions();
  //login action done by user do stuff if successful
  var dataStatus = await loginActions.SendDataForCustomerLogin(JSON.stringify(result));
  if(dataStatus)
  {
    var userDetails = await loginActions.GetCustomerIdByName(paramList);
    ///console.log(userDetails);
    sessionStorage.setItem("userDetails",userDetails['result']);
    HTMLFormatter.makeUserActionVisible(username);
  }
}
