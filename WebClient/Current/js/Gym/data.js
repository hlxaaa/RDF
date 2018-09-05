var orderField = 'KM'
var desc = true;
var listSort;
var defaultField = 'KM'
var page = 1;
var time;
var dateStr;

var lastName;
var ths = ''
var isAdd = true;


var emptyHtml = `    <div class="info info-Blue blue ">
        <img class="info-headImg fl" src="../Images/Gym/headportrait.png" />
        <div class="info-data fl">
            <img class="img-go" src="../Images/Gym/go.png" />
        </div>
        <div class="info-thisTime info-thisTime-fitnow">
        </div>
        <div class="fc"></div>
    </div>`

$(document).ready(function () {

    $('body').delegate('.info-data1', 'click', function () {
        orderField = $(this).data('type')
        changePage();
    })

    $('body').delegate('.info-time', 'click', function () {
        orderField = "TotalTime";
        changePage();
    })

    var t = setInterval(changePage, 3 * 1000)
    changePage();
})

function changePage() {
    var data1 = {
        search: $('.input-search').val(),
        pageIndex: 1,
        orderField: orderField,
        isDesc: desc,
        size: 1,
        //gymId: $('.select-gymId').val()
    }
    jQuery.postNL('../gymAjax/GetGymDataAll', data1, function (data) {
        var data = data.ListData;
        var h = "";
        for (var i = 0; i < data.length; i++) {
            //console.log(data[i].TotalTime!=null)
            if (data[i].TotalTime != null) {
                var color = "orange";
                if (parseInt(data[i].XL) < 90)
                    color = "green"
                else if (parseInt(data[i].XL) >= 90 && parseInt(data[i].XL) < 160)
                    color = "blue"
                h += GetInfoHtml(color, data[i])
                //console.log(h);
            } else {
                h += emptyHtml;
            }
        }
        $('.info').remove();
        //console.log(h)
        $('body').append(h)

    })

}

function GetInfoHtml(color, data) {
    var h = ` <div class="info info-${color} ${color} ">
        <img class="info-headImg fl" src="${data.Url}" />
        <div class="info-data fl">
            <div class="info-data1 fl" data-type="KM">
                <div class="info-data-name ft12 fl">
                    <div>里程</div>
                    <div>DIST</div>
                </div>
                <div class=" info-data-no ft20 fl">${data.KM}<span class="info-data-tip">KM</span></div>
            </div>
            <div class="info-data1 fl" data-type="DRAG">
                <div class="info-data-name ft12 fl">
                    <div>阻力</div>
                    <div>DRAG</div>
                </div>
                <div class=" info-data-no ft20 fl">${data.DRAG}<span class="info-data-tip">LB</span></div>
            </div>
            <div class="info-data1 fl" data-type="SD">
                <div class="info-data-name ft12 fl">
                    <div>速度</div>
                    <div>SPEED</div>
                </div>
                <div class=" info-data-no ft20 fl">${data.SD}<span class="info-data-tip">KM/H</span></div>
            </div>
            <div class="info-data1 fl" data-type="WATT">
                <div class="info-data-name ft12 fl">
                    <div>瓦特</div>
                    <div>WATT</div>
                </div>
                <div class=" info-data-no ft20 fl">${data.WATT}</div>
            </div>
            <div class="info-data1 fl" data-type="CAL">
                <div class="info-data-name ft12 fl">
                    <div>卡路里</div>
                    <div>CAL</div>
                </div>
                <div class=" info-data-no ft20 fl">${data.CAL}</div>
            </div>
            <div class="info-data1 fl" data-type="XL">
                <div class="info-data-name ft12 fl">
                    <div>心率</div>
                    <div>RATE</div>
                </div>
                <div class=" info-data-no ft20 fl">${data.XL}</div>
            </div>
        </div>
        <div class="info-thisTime info-thisTime-${color} ">
          
        </div>
<div class="info-time-wrapper">
  <div class="info-time fr  ft48">${data.TotalTime}"</div>
</div>
        <div class="fc"></div>
    </div>`
    return h;
}