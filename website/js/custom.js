var inviteToGiftDict={};
var selectionDicts={};
var occassionTypeUrl;

class test
{

}
function getRestUrl(selectItem)
{
  result={};
  if(selectItem=="occassion")
  {
    result["url"]="https://localhost:5001/CustomerAction/occassionTypes";
    result["select"]=`<select  id="createEventOccasionSelect" name="occasionList" size="1" onchange="occassionTypeSelected()" required>
    <option value="--Select--">--Select--</option>
    {optionlist}
    </select>    `
  }
  else if (selectItem=="allgifts")
  {
    result["url"]="https://localhost:5001/CustomerAction/allgifts";
    result["select"]=`<select id="expectedGiftListSelect" name="giftExpectedList" size="3" multiple required>
    {optionlist}
    </select>`;
  }
  return result;
}
async function createEvent1()
{
  //populating occasions
 await ConvertJsonToOption("occassion","createEventOccasionDiv");
  //reading  options
 await ConvertJsonToOption("allgifts","expectedGiftListDiv");
  //show just events
  showOneFormAlone("createEvent");
}
function inviteUser()
{
  var inviteUserEventList=`<select  id="eventListSelect" name="eventList" size="1" required>`
      +ConvertJsonToOption("eventList")
      +`</select>`

    var inviteUserCustomerList=  `<select id="inviteUserCustomerListSelect" name="customerList" size="1" required>`
          +ConvertJsonToOption("customerList")
          +`</select>`
  document.getElementById("eventListDiv").innerHTML=inviteUserEventList;
  document.getElementById("inviteUserCustomerListDiv").innerHTML=inviteUserCustomerList;
  showOneFormAlone("InviteUser");
}
function modifyEvent()
{
  //reading  options
  var modifyEventEventList=`<select  id="modifyEventeventListSelect" name="eventList" size="1" required>`
      +ConvertJsonToOption("eventList")
      +`</select>`
    var  modifyEventExpectedGiftList=`<select id="modifyEventExpectedGiftListSelect" name="giftExpectedList" size="3" multiple required>
    `+ConvertJsonToOption("expectedGifts")+
    `  </select>`;
    document.getElementById("modifyEventeventListDiv").innerHTML=modifyEventEventList;
    document.getElementById("modifyEventexpectedGiftListDiv").innerHTML=modifyEventExpectedGiftList;
    showOneFormAlone("modifyEvent");
}
function BuyGiftForInvite()
{
  var buyGiftForInviteInviteList=`<select  id="BuyGiftForInviteInviteListSelect" name="eventList" size="1" onchange="buyGiftForInviteInviteListSelectChanged()" required>`
      +ConvertJsonToOption("inviteList")
      +`</select>`
  document.getElementById("BuyGiftForInviteInviteListDiv").innerHTML=buyGiftForInviteInviteList;
buyGiftForInviteInviteListSelectChanged();
    showOneFormAlone("BuyGiftForInvite");
}
function buyGiftForInviteInviteListSelectChanged()
{
  var  buyGiftForInviteExpectedGiftList=`<select id="BuyGiftForInviteExpectedGiftSelect" name="giftExpectedList" required>
  `+ConvertJsonToOption("buyGiftinviteListexpectedGifts")+
  `  </select>`;

  document.getElementById("BuyGiftForInviteExpectedGiftDiv").innerHTML=buyGiftForInviteExpectedGiftList;
}
function RemoveGiftForInvite()
{
  var removeGiftForInviteInviteList=`<select  id="RemoveGiftForInviteinviteListSelect" name="eventList" size="1" onchange="removeGiftForInviteInviteListSelectChanged()" required>`
      +ConvertJsonToOption("inviteList")
      +`</select>`
  document.getElementById("RemoveGiftForInviteinviteListDiv").innerHTML=removeGiftForInviteInviteList;
  removeGiftForInviteInviteListSelectChanged();
  showOneFormAlone("RemoveGiftForInvite");
}
function removeGiftForInviteInviteListSelectChanged()
{
  var  removeGiftForInviteExpectedGiftList=`<select id="RemoveGiftForInviteGiftListSelect" name="giftExpectedList" required>
  `+ConvertJsonToOption("removeGiftinviteListexpectedGifts")+
  `  </select>`;
  document.getElementById("RemoveGiftForInviteGiftListDiv").innerHTML=removeGiftForInviteExpectedGiftList;
}

function RespondToInvite()
{
  var removeGiftForInviteInviteList=`<select  id="RespondToInviteInviteListSelect" name="inviteList" size="1" required>`
      +ConvertJsonToOption("inviteList")
      +`</select>`
  document.getElementById("RespondToInviteInviteListDiv").innerHTML=removeGiftForInviteInviteList;
  showOneFormAlone("RespondToInvite");
}
function ChangePassword()
{

  showOneFormAlone("ChangePassword");
}


function showOneFormAlone(formIdInQ)
{
    formIdList =["InviteUser","modifyEvent","BuyGiftForInvite", "RemoveGiftForInvite","RespondToInvite","ChangePassword","createEvent","loginScreenDiv"];
    for(formId in formIdList)
    {
        if(formIdInQ==formIdList[formId])
          continue;
        document.getElementById(formIdList[formId]).setAttribute("style", "display:none");
    }
    if(formIdInQ=="none")
      return;
    document.getElementById(formIdInQ).setAttribute("style", "display:block");
}

async function ConvertJsonToOption(selectItem, divName)
{
  result = getRestUrl(selectItem);
  var url = result["url"];
  console.log("fetching url "+url);
  const response = await fetch(url);
  const json = await response.json();
  console.log(json);
  selectionDicts[selectItem]=json;
  options={};
  var resultText="";
  var part1='<option value="';
  var part2='">';
  var part3='</option>';
  for (var key in json)
  {
      resultText += part1+key+part2+json[key]+part3;
  }
  resultText = result["select"].replace("{optionlist}", resultText);
  console.log(resultText);
  console.log(divName);
  document.getElementById(divName).innerHTML=resultText;
}

function GetOptionsForMenu(selectItem)
{
  var optionList={};
  var sampleList={};
  if(selectItem=="expectedGifts")
  {
    optionList["abc"] = "Plates";
    optionList["d"] = "Shoes";
    optionList["f"] = "Speaker";
    optionList["g"] = "Oven";
  }
  else if(selectItem=="eventList")
  {
    optionList["event1Id"] ="Marriage on 22nd";
    optionList["event2Id"] ="Birthday on 23nd";
    optionList["event3Id"] ="HouseWarming on 12th";
  }
  else if(selectItem=="customerList")
    {
      optionList["a"] ="Jeff Bridges";
      optionList["b"] ="Sonny corleno";
      optionList["c"] ="mira raghunath";
    }
    else if(selectItem=="inviteList")
      {
        sampleList["invite1"]={};
        sampleList["invite2"]={};
        sampleList["invite3"]={};
        sampleList["invite1"]["eventName"] ="Marriage on 22nd";
        sampleList["invite1"]["gifts"] =[{"1":"apple","2":"orange","3":"banana"}];
        sampleList["invite2"]["eventName"] ="Birthday on 4th";
        sampleList["invite2"]["gifts"] =[{"1":"tomate","2":"potato","3":"capsicum"}];
        sampleList["invite3"]["eventName"] ="HouseWarming on 19th";
        sampleList["invite3"]["gifts"] =[{"1":"mice","2":"rat","3":"cat"}];
        inviteToGiftDict=sampleList;
        for(val in sampleList)
        {
          optionList[val]=sampleList[val]["eventName"];
        }
      }
    else if(selectItem=="buyGiftinviteListexpectedGifts")
    {
      var invite =document.getElementById("BuyGiftForInviteInviteListSelect");
      var selectedInvite = invite.options[invite.selectedIndex].value;
      optionList=inviteToGiftDict[selectedInvite]["gifts"][0];
    }
    else if(selectItem=="removeGiftinviteListexpectedGifts")
    {
      var invite =document.getElementById("RemoveGiftForInviteinviteListSelect");
      var selectedInvite = invite.options[invite.selectedIndex].value;
      optionList=inviteToGiftDict[selectedInvite]["gifts"][0];
    }
    else if(selectItem=="occassionlist")
    {
      optionList=getFromRestAPI();
    }
      alert(JSON.stringify(optionList));
  return JSON.stringify(optionList);

}

function CreateEventDone() {
document.getElementById("createEvent").setAttribute("style", "display:none");
    alert("donestuff");
}

function ValidateEventForm() {
    var isValid=true;
    var occassion =document.getElementById("createEventOccasionSelect")
    var selectedOccasion = occassion.options[occassion.selectedIndex].value;
    var eventPlace = document.getElementById("eventPlace").value;
    var person1 = document.getElementById("createEventOccasionPerson1name").value;
    var person2 = document.getElementById("createEventOccasionPerson2name").value;
    var eventDate = document.getElementById("eventDate").value;
    var eventTime = document.getElementById("eventTime").value;
    var giftList =  $('#expectedGiftListSelect').val();
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
    if(isValid)
      SendObjectToAPI("CreateEvent", result);
    return isValid;
}

function InviteUserValidateForm()
{
  var eventlist =document.getElementById("eventListSelect")
  var selectedEvent = eventlist.options[eventlist.selectedIndex].value;
  var custList =document.getElementById("inviteUserCustomerListSelect")
  var selectedCustomer = custList.options[custList.selectedIndex].value;
  var inviteCustomerDict ={};
  inviteCustomerDict[selectedEvent]=selectedCustomer;
  var result = JSON.stringify(inviteCustomerDict);
  SendObjectToAPI("customerInvitedForEvent", result);
}
function modifyEventValidateForm() {
  var isValid=true;
  var eventList =document.getElementById("modifyEventeventListSelect")
  var selectedEvent = eventList.options[eventList.selectedIndex].value;
  var eventPlace = document.getElementById("modifyeventPlace").value;
  var eventDate = document.getElementById("modifyeventDate").value;
  var eventTime = document.getElementById("modifyeventTime").value;
  var giftList = $('#modifyEventExpectedGiftListSelect').val();
  var event1={};
  event1["eventId"]=selectedEvent;
  event1["place"]=eventPlace;
  event1["date"]=eventDate;
  event1["time"]=eventTime;
  event1["expectedGiftList"]=giftList;
  var result = JSON.stringify(event1);
  SendObjectToAPI("modifyEvent", result);
}

function buyGiftForInviteValidateForm()
{
  var inviteList =document.getElementById("BuyGiftForInviteInviteListSelect")
  var invite = inviteList.options[inviteList.selectedIndex].value;
  var giftList =document.getElementById("BuyGiftForInviteExpectedGiftSelect")
  var gift = giftList.options[giftList.selectedIndex].value;
  result={};
  result[invite]=gift;
  SendObjectToAPI("buyGiftForInvite", JSON.stringify(result));
}
function removeGiftForInviteValidateForm()
{
  var inviteList =document.getElementById("RemoveGiftForInviteinviteListSelect")
  var invite = inviteList.options[inviteList.selectedIndex].value;
  var giftList =document.getElementById("RemoveGiftForInviteGiftListSelect")
  var gift = giftList.options[giftList.selectedIndex].value;
  result={};
  result[invite]=gift;
  SendObjectToAPI("removeGiftForInvite", JSON.stringify(result));
}
function respondToInviteValidateForm()
{
  var inviteList =document.getElementById("RespondToInviteInviteListSelect")
  var invite = inviteList.options[inviteList.selectedIndex].value;
  var choiceList =document.getElementById("RespondToInviteChoiceListSelect")
  var choice = choiceList.options[choiceList.selectedIndex].value;
  result={};
  result[invite]=choice;
  SendObjectToAPI("respondToInvite", JSON.stringify(result));
}
function changePasswordValidateForm()
{
    var newPwd=document.getElementById("changePasswordNewPassword").value;
    var confirmPwd=document.getElementById("changePasswordConfirmPassword").value;
    if(newPwd!=confirmPwd)
    {
      document.getElementById("passwordMismatchLabel").setAttribute("style","display:inline;  background-color: red;");
      return false;
    }
    result={};
    result["password"]=confirmPwd;
    SendObjectToAPI("changepassword", JSON.stringify(result));
    return true;
}
function loginScreenValidateForm()
{
  var username=document.getElementById("loginScreenUsername").value;
  var password=document.getElementById("loginScreenPassword").value;
  result={};
  result["id"]="";
  result["username"]=username;
  result["password"]=password;
  sessionStorage.setItem("userDetails", result);
  SendObjectToAPI("login",JSON.stringify(result));
  return true;
}
function SendObjectToAPI(type, result)
{
  if(type=="CreateEvent")
  {
    alert(result);
    SendDataForEventCreation(result);
  }
  if(type=="customerInvitedForEvent")
  {
    alert(result);
  }
  if(type=="modifyEvent")
  {
    alert(result);
  }
  if(type=="buyGiftForInvite")
  {
    alert(result);
  }
  if(type=="removeGiftForInvite")
  {
    alert(result);
  }
  if(type=="respondToInvite")
  {
    alert(result);
  }
  if(type=="changepassword")
  {
    alert(result)
  }
  if(type=="login")
  {
    //alert(result);
    SendDataForCustomerLogin(result);
  }
}
async function SendDataForEventCreation(result)
{
  alert("here");
  var statusOfEvent= postDataToServer(result,'https://localhost:5001/CustomerAction/createEvent');
  if(statusOfEvent)
    console.log("Event created successfully");
}
async function SendDataForCustomerLogin(result)
{
    var name = JSON.parse(result)["username"];
    var dataStatus =await postDataToServer(result, 'https://localhost:5001/CustomerAction/login');
    if(dataStatus)
    {
      GetCustomerIdByName(name);
    }
}
async function postDataToServer(result, url)
{
  var dataStatus=false;
    await fetch(url, {
      method: 'POST', // or 'PUT'
      mode: 'cors',
      headers: {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin':url
      },
      body: result,
    })
    .then(response => {
      console.log('Success:', response);
      dataStatus=true;
    })
    .catch((error) => {
      console.error('Error:', error);
    });
    return dataStatus;
}
async function GetCustomerIdByName(customerName)
{
  var getIdUrl="https://localhost:5001/CustomerAction/customerName="+customerName;
  console.log(getIdUrl);
  const response = await fetch(getIdUrl);
  const json = await response.json();
  console.log(json);
  sessionStorage["customerId"] = json["id"];
  makeUserActionVisible(json["username"]);
}
function makeUserActionVisible(username)
{
  if(sessionStorage.getItem("userDetails")!=null)
  {
    showOneFormAlone("none");
    document.getElementById("UserActionMenu").setAttribute("style","display:inline");
    document.getElementById("UserActionMenu").innerHTML=  username +"'s Action(s)";
    document.getElementById("CustomerActionMenu").classList.remove('disabled');
  }
  else
    document.getElementById("UserActionMenu").setAttribute("style","display:none");
}
function occassionTypeSelected()
{
  var occassion =document.getElementById("createEventOccasionSelect")
  var selectedOccasion = selectionDicts["occassion"][occassion.selectedIndex];
  var noSelect=occassion.selectedIndex;
  if(noSelect==0)
  {
      document.getElementById("createEventOccasionPerson2Div").classList.remove('d-flex');
      document.getElementById("createEventOccasionPerson1Div").classList.remove('d-flex');
      document.getElementById("createEventOccasionPerson2Div").classList.add('d-none');
      document.getElementById("createEventOccasionPerson1Div").classList.add('d-none');
  }
  else if(selectedOccasion=='Marriage')
  {
    document.getElementById("createEventOccasionPerson1Div").classList.remove('d-none');
    document.getElementById("createEventOccasionPerson2Div").classList.remove('d-none');
    document.getElementById("createEventOccasionPerson2Div").classList.add('d-flex');
    document.getElementById("createEventOccasionPerson1Div").classList.add('d-flex');
  }
  else if(selectedOccasion=='BirthDay' || selectedOccasion=='HouseWarming')
  {
    document.getElementById("createEventOccasionPerson2Div").classList.remove('d-none');
    document.getElementById("createEventOccasionPerson2Div").classList.add('d-flex');
    document.getElementById("createEventOccasionPerson1Div").classList.add('d-none');
    document.getElementById("createEventOccasionPerson1Div").classList.remove('d-flex');
  }
}
