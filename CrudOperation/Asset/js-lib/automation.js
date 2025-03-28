$(".triggerBlock").click(function () {
    var triggerVal = $(this).attr("data-val");
    $(".actions").html('<div class="blockData focus">' +
        '<div class="blockHeader parentTrigger" > Start When</div>' +
        '<div class="blockDetail">' +
        '<button type="button" class="btn btn-outline-secondary text-truncate">' + triggerVal + '</button>' +
        '</div>' +
        '</div><div class="verticalLine"></div>' +
        '<div style="display: flex;">' +
        '<div>' +
        '<div class="blockData ">' +
        '<div class="blockHeader"> If </div>' +
        '<div class="blockDetail if-condition-button">' +
        '<button type="button" class="btn btn-primary" onclick="showCondition()"><i class="fe-plus"></i> Add Condition</button>' +
        '</div>' +
        ' </div>' +
        '<div class="yesLine">Yes</div>' +
        '<div class="blockData">' +
        '<div class="blockHeader"> Then </div>' +
        '<div class="blockDetail yes-action-button">' +
        '<button type="button" class="btn btn-primary" onclick="showActions()"><i class="fe-plus"></i> Add Action</button>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '<div class="noLine">No</div>' +
        '<div class= "blockData"> ' +
        '<div class= "blockHeader"> Then </div > ' +
        '<div class= "blockDetail no-action-button"> ' +
        '<button type = "button" class= "btn btn-primary" onclick="showActions()"> <i class="fe-plus"></i> Add Action</button > ' +
        '</div> ' +
        '</div> ' +
        '</div> ' +
        '<div class="addMore"><div class= "verticalLine" ></div> ' +
        '<img src="/assets/themes/omnicxv3/images/plus-svg.svg" onclick="appendCondition()" alt="" style="margin-left: 6rem; margin-top: -0.41rem; cursor: pointer;"></div>');
});
//FUNCTION FOR ADD MORE IF CONDITION
function appendCondition() {
    $(".addMore").html('<div class="verticalLine"></div>' +
        '<div style="display: flex;">' +
        '<div>' +
        '<div class="blockData ">' +
        '<div class="blockHeader"> If </div>' +
        '<div class="blockDetail if-condition-button">' +
        '<button type="button" class="btn btn-primary" onclick="showCondition()"><i class="fe-plus"></i> Add Condition</button>' +
        '</div>' +
        ' </div>' +
        '<div class="yesLine">Yes</div>' +
        '<div class="blockData">' +
        '<div class="blockHeader"> Then </div>' +
        '<div class="blockDetail yes-action-button">' +
        '<button type="button" class="btn btn-primary" onclick="showActions()"><i class="fe-plus"></i> Add Action</button>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '<div class="noLine">No</div>' +
        '<div class= "blockData"> ' +
        '<div class= "blockHeader"> Then </div > ' +
        '<div class= "blockDetail no-action-button"> ' +
        '<button type = "button" class= "btn btn-primary" onclick="showActions()"> <i class="fe-plus"></i> Add Action</button > ' +
        '</div> ' +
        '</div> ' +
        '</div> ' +
        '<div class="addMore"><div class= "verticalLine" ></div> ' +
        '<img src="/assets/themes/omnicxv3/images/plus-svg.svg" onclick="appendCondition()" alt="" style="margin-left: 6rem; margin-top: -0.41rem; cursor: pointer;"></div>');
}

function showCondition() {
    $(".automation-conditions").addClass("show");
    $(".automation-actions, .automation-triggers, .automation-action-form, .automation-condition-form").removeClass("show");
}
function showActions() {
    $(".automation-actions").addClass("show");
    $(".automation-conditions, .automation-triggers, .automation-action-form, .automation-condition-form").removeClass("show");
}
function showTriggers() {
    $(".automation-triggers").addClass("show");
    $(".automation-actions, .automation-conditions, .automation-action-form, .automation-condition-form").removeClass("show");
}

$(".conditionBlock").click(function () {
    var conditionVal = $(this).attr("data-val");
    $(".automation-condition-form").addClass("show");
    $(".automation-triggers, .automation-conditions, .automation-actions, .automation-action-form").removeClass("show");
    $(".if-condition-button").html('<div class="blockDetail"><button type = "button" class= "btn btn-outline-secondary text-truncate">' + conditionVal + '</button ></div>');
});

$(".actionBlock").click(function () {
    var actionVal = $(this).attr("data-val");
    $(".automation-action-form").addClass("show");
    $(".automation-triggers, .automation-conditions, .automation-actions, .automation-condition-form").removeClass("show");
    $(".no-action-button").html('<div class="blockDetail"><button type = "button" class= "btn btn-outline-secondary text-truncate"> ' + actionVal + '</button ></div>');
});