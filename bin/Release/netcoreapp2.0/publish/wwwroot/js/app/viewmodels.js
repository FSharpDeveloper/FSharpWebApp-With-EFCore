ko.bindingHandlers.bootstrapTable = {
    init: function(element, valueAccessor, allBindingsAccessor) {
      var source = allBindingsAccessor().bootstrapTable.source();
      var keys = Object.keys(source[0]);
      var columns = keys.map(k => {
        return {
          field: k,
          title: k,
          sortable: true
        }
      });
  
      var data = source.reduce((data, item) => {
        data.push(ko.toJS(item));
        return data;
      }, []);
  
      var config = {
        columns: columns,
        data: data,
        search: true
      };
  
      $(element).bootstrapTable(config);
    }
  };
  
  // constructor for each 'sale'
  function Group(group) {
    var self = this;
    self.GroupId = ko.observable(group.groupId);
    self.Groupname = ko.observable(group.groupname);
    self.Description = ko.observable(group.description);
    self.Members = ko.observableArray(group.members);
  }
  
  // some fake data
  var data = [
    {
      "NAME": "Alice",
      "PHONE": 123,
      "EMAIL": "alice@alice.com",
      "ITEM": "stuff",
      "DESCRIPTION": "some stuff",
      "SALE_ID": 1
    },
    {
      "NAME": "John",
      "PHONE": 345,
      "EMAIL": "john@john.com",
      "ITEM": "stuff 2",
      "DESCRIPTION": "some stuff 2",
      "SALE_ID": 2
    }
  ];
  
  var ViewModel = function() {
    var self = this;
    // self.Groups = ko.observableArray([]);
    $.get("/groups/loadgroups", success =>{
        self.Groups = ko.observableArray(success); // map a list of sales
    });  
  };
  
  var vm = new ViewModel();
  ko.applyBindings(new ViewModel(), document.getElementById('groups'));