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
    else if(type=='getCustomerById')
        return 'https://localhost:5001/CustomerAction';
    else if(type='createEvent')
      return 'https://localhost:5001/CustomerAction/createEvent';
  }
  static GetParameterListForType(type)
  {
    if(type=='getCustomerByName')
    {
      return ['customerName'];
    }
    if(type=='getCustomerById')
    {
      return ['customer_id'];
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
    static occassionTypeSelected()
    {
      var occassion =document.getElementById("createEventOccasionListSelect")
      var selectedOccasion = occassion.options[occassion.selectedIndex].innerHTML;
      //console.log( StaticObjectHolder.KVPHolder["occassionDict"]['result']);
      //console.log(selectedOccasion);
      if(selectedOccasion=='--Select--')
      {
          HTMLFormatter.MakeDivInVisible("createEventOccasionPerson2Div");
          HTMLFormatter.MakeDivInVisible("createEventOccasionPerson1Div");
      }
      else if(selectedOccasion=='Marriage')
      {
          HTMLFormatter.MakeDivVisible("createEventOccasionPerson2Div");
          HTMLFormatter.MakeDivVisible("createEventOccasionPerson1Div");
      }
      else if(selectedOccasion=='BirthDay' || selectedOccasion=='HouseWarming')
      {
          HTMLFormatter.MakeDivVisible("createEventOccasionPerson2Div");
          HTMLFormatter.MakeDivInVisible("createEventOccasionPerson1Div");
      }
    }
    static MakeDivVisible(divName)
    {
      document.getElementById(divName).classList.remove('d-none');
      document.getElementById(divName).classList.add('d-flex');
    }
    static MakeDivInVisible(divName)
    {
      document.getElementById(divName).classList.remove('d-flex');
      document.getElementById(divName).classList.add('d-none');
    }
  }

class HTMLSnippets
{
  GetSelectList(selectListId, isMultiple, maxNumberOfSelects, optionDict, onChangeFunction="default", isSelectNeeded=false)
  {
    var onChangeFunctionText= "";
    if(onChangeFunction!='default')
    {
        onChangeFunctionText="onchange='"+onChangeFunction+"()'";
    }
    var initSelect = `<select id='`+selectListId+`' name='`+selectListId+`' `+ onChangeFunctionText +`size='`;
    if(isMultiple)
      initSelect = initSelect + maxNumberOfSelects+`' multiple required> {optionList} </select>`;
    else
      initSelect = initSelect + `1' required> {optionList} </select>`;
    var resultText='';
    var part1=`<option value='`;
    var part2=`'>`;
    var part3=`</option>`;
    if(isSelectNeeded)
    {
      optionDict['--Select--']='--Select--';
    }

    for (var key in optionDict)
    {
      if(key=='--Select--')
        part2=`' selected="selected">`
      else
        part2=`'>`;
        resultText += part1+key+part2+optionDict[key]+part3;
    }
    //console.log(resultText);
    initSelect = initSelect.replace('{optionList}', resultText);
    return initSelect;
  }
}
class HTMLDisplay
{
  static async createEvent1()
  {
    var divElementOccassionPreText="createEventOccasionList";
    var divElementEventPreText="createEventExpectedGiftList";
    var htmlSnippets = new HTMLSnippets();
    var uriProvider = new URIProvider();
    //populating occasions
    var occassionDict =await ServerInteraction.getData(URIProvider.GetURIForOccassion("occassion"));
    document.getElementById(divElementOccassionPreText+"Div").innerHTML=htmlSnippets.GetSelectList(divElementOccassionPreText+"Select", false, 3,occassionDict['result'],"HTMLFormatter.occassionTypeSelected", true);
    StaticObjectHolder.KVPHolder['occassionDict']=occassionDict;
    //reading  gifts
    var giftDict = await ServerInteraction.getData(URIProvider.GetURIForOccassion("allgifts"));
    document.getElementById(divElementEventPreText+"Div").innerHTML=htmlSnippets.GetSelectList(divElementEventPreText+"Select", true, 3,giftDict['result']);

    //show just create event
   HTMLFormatter.showOneFormAlone("createEvent");
  }
  static async inviteUser()
  {

      HTMLFormatter.showOneFormAlone("InviteUser");
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
class EventActions
{
  async SendDataForEventCreation(result)
  {
    var createEventUri =URIProvider.GetURIForOccassion("createEvent");
    console.log("Sending data for event creation to url "+createEventUri);
    var statusOfEvent= await ServerInteraction.postData(result,createEventUri);
    if(statusOfEvent)
    {
      console.log("Event created successfully");
    }
    else
    {
      console.log('Event creation failed');
    }
  }
}
class StaticObjectHolder
{
  static KVPHolder = {};
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
async function ValidateEventForm() {
    var isValid=true;
    var occassion =document.getElementById("createEventOccasionListSelect")
    var selectedOccasion = occassion.options[occassion.selectedIndex].value;
    var eventPlace = document.getElementById("eventPlace").value;
    var person1 = document.getElementById("createEventOccasionPerson1name").value;
    var person2 = document.getElementById("createEventOccasionPerson2name").value;
    var eventDate = document.getElementById("eventDate").value;
    var eventTime = document.getElementById("eventTime").value;
    var giftList =  $('#createEventExpectedGiftListSelect').val();
    //do all kind of validations here.
    if(selectedOccasion=='--Select--')
        document.getElementById("createEventOccasionLabel").setAttribute("style","color:red")
    var event1={};
    event1["occassion"]=selectedOccasion;
    event1["place"]=eventPlace;
    event1["date"]=eventDate;
    event1["time"]=eventTime;
    event1["person1"]=person1;
    event1["person2"]=person2;
    event1["giftIds"]=giftList;
    var result = JSON.stringify(event1);
    var eventActions = new EventActions();
    if(isValid)
      await eventActions.SendDataForEventCreation(result);
    HTMLFormatter.showOneFormAlone("none");
}
