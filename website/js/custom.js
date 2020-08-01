function createEvent1()
{
  //reading  options
  var  createEventExpectedGiftList=`<select id="expectedGiftListSelect" name="giftExpectedList" size="3" multiple required>
    `+ConvertJsonToOption("expectedGifts")+
    `  </select>`;
  document.getElementById("expectedGiftListDiv").innerHTML=createEventExpectedGiftList;
//showing form
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
function showOneFormAlone(formIdInQ)
{
    formIdList =["InviteUser","modifyEvent","BuyGiftForInvite", "RemoveGiftForInvite","RespondToInvite","ChangePassword","createEvent"];
    for(formId in formIdList)
    {
        if(formIdInQ==formIdList[formId])
          continue;
        document.getElementById(formIdList[formId]).setAttribute("style", "display:none");
    }
    document.getElementById(formIdInQ).setAttribute("style", "display:block");
}

function ConvertJsonToOption(selectItem)
{
  var optionListJson = GetOptionsForMenu(selectItem);
  var resultText="";
  var part1='<option value="';
  var part2='">';
  var part3='</option>';
  var options = JSON.parse(optionListJson);
  for (var key in options) {
    resultText += part1+key+part2+options[key]+part3;
}
return resultText;
}

function GetOptionsForMenu(selectItem)
{
  var optionList={};
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
  return JSON.stringify(optionList);
}


function CreateEventDone() {
document.getElementById("createEvent").setAttribute("style", "display:none");
    alert("donestuff");
}
function ValidateEventForm() {
    var isValid=true;
    var occassion =document.getElementById("occasion")
    var selectedOccasion = occassion.options[occassion.selectedIndex].value;
    var eventPlace = document.getElementById("eventPlace").value;
    var eventDate = document.getElementById("eventDate").value;
    var eventTime = document.getElementById("eventTime").value;
    var giftList =  $('#expectedGiftListSelect').val();
    var event1={};
    event1["occassion"]=selectedOccasion;
    event1["place"]=eventPlace;
    event1["date"]=eventDate;
    event1["time"]=eventTime;
    event1["expectedGiftList"]=giftList;
    var result = JSON.stringify(event1);
    SendObjectToAPI("event", result);

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

function SendObjectToAPI(type, result)
{
  if(type=="event")
  {
    alert(result);
  }
  if(type=="customerInvitedForEvent")
  {
    alert(result);
  }
  if(type=="modifyEvent")
  {
    alert(result);
  }
}
function ValidateInviteForm()
{

}
function ChangePassword()
{
  showOneFormAlone("ChangePassword");
}



function BuyGiftForInvite()
{
    showOneFormAlone("BuyGiftForInvite");
}

function RemoveGiftForInvite()
{
  showOneFormAlone("RemoveGiftForInvite");
}

function RespondToInvite()
{
  showOneFormAlone("RespondToInvite");
}
