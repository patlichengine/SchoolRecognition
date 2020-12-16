$('[name=duallistbox_demo1]').bootstrapDualListbox({

    // default text
    filterTextClear: 'show all',
    filterPlaceHolder: 'Filter',
    moveSelectedLabel: 'Move selected',
    moveAllLabel: 'Move all',
    removeSelectedLabel: 'Remove selected',
    removeAllLabel: 'Remove all',

    // true/false (forced true on androids, see the comment later)
    moveOnSelect: true,

    // 'all' / 'moved' / false                                           
    preserveSelectionOnMove: false,

    // 'string', false                                     
    selectedListLabel: false,

    // 'string', false
    nonSelectedListLabel: false,

    // 'string_of_postfix' / false                                                      
    helperSelectNamePostfix: '_helper',

    // minimal height in pixels
    selectorMinimalHeight: 100,

    // whether to show filter inputs
    showFilterInputs: true,

    // string, filter the non selected options                                                 
    nonSelectedFilter: '',

    // string, filter the selected options                                              
    selectedFilter: '',

    // text when all options are visible / false for no info text                      
    infoText: 'Showing all {0}',

    // when not all of the options are visible due to the filter                                         
    infoTextFiltered: '<span class="badge badge-warning">Filtered</span> {0} from {1}',

    // when there are no options present in the list
    infoTextEmpty: 'Empty list',

    // sort by input order
    sortByInputOrder: false,

    // filter by selector's values, boolean
    filterOnValues: false,

    // boolean, allows user to unbind default event behaviour and run their own instead                                                   
    eventMoveOverride: false,

    // boolean, allows user to unbind default event behaviour and run their own instead                                                
    eventMoveAllOverride: false,

    // boolean, allows user to unbind default event behaviour and run their own instead
    eventRemoveOverride: false,

    // boolean, allows user to unbind default event behaviour and run their own instead                                              
    eventRemoveAllOverride: false,

    // sets the button style class for all the buttons
    btnClass: 'btn-outline-secondary',

    // string, sets the text for the "Move" button                                            
    btnMoveText: '&gt;',

    // string, sets the text for the "Remove" button                                                        
    btnRemoveText: '&lt;',

    // string, sets the text for the "Move All" button
    btnMoveAllText: '&gt;&gt;',

    // string, sets the text for the "Remove All" button
    btnRemoveAllText: '&lt;&lt;'

});