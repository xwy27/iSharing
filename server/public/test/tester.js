$(function () {
  var OL_Action_Root = "http://localhost:8000";
  var action = $('#url')[0],
    content = $('#content')[0],
    checkbox = $('#bool')[0],
    file = $('#file')[0],
    returnjson = $('#returnjson')[0];


  $('.submit').click(function () {
    if (checkbox.checked){
      fm = new FormData();
      fm.append(file, file.files[0]);
      $.ajax({
        data: fm,
        url: OL_Action_Root + action.value,
        dataType: 'json',
        contentType: 'multipart/form-data',
        processData: false,
        cache: false,
        timeout: 5000,
        type: "POST",
        success: function (data) {
          returnjson.innerText = JSON.stringify(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
          console.log(jqXHR);
        }
      });
    }
    else {
      $.ajax({
        data: JSON.stringify(eval('(' + content.value + ')')),
        url: OL_Action_Root + action.value,
        dataType: 'json',
        cache: false,
        timeout: 5000,
        processData: true,
        contentType: 'application/json',
        type: "POST",
        success: function (data) {
          returnjson.innerText = JSON.stringify(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
          console.log(jqXHR);
        }
      });
    }

    return false;
  })


});