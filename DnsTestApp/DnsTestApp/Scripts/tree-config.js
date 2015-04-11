$(function () {
    $('#tree')
        .jstree({
            'types': {
                'default': {},
                'file': { 'valid_children': [] }
            },
            'core': {
                'data': {
                    'url': 'TreeHandler.ashx?operation=get_node',
                    'dataType': 'JSON',
                    'data': function (node) {
                        return { 'id': node.id };
                    },
                    "error": function() {
                        $('#alertdiv').removeClass('hidden');
                        window.setTimeout(function () {
                            $("#alertdiv").fadeTo(500, 0).slideUp(500, function () {
                                $(this).addClass("hidden");
                            });
                        }, 5000);
                    }
                },
                "check_callback" : function(operation, node, nodeParent, nodePosition, more) {
                    console.log(nodeParent.id);
                    return nodeParent.id !== '#';
                },
                'themes': {
                    'responsive': false
                }
            },
            'plugins': ["themes", "dnd", "wholerow", "types"]
        }).on('move_node.jstree', function (e, data) {
            $.get('TreeHandler.ashx?operation=move_node', { 'id': data.node.id, 'parent': data.parent, 'position': data.position })
                .fail(function () {
                    data.instance.refresh();
                });
        });

});