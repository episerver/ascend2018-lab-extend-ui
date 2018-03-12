var publishEvent = function () {
  if (window.epi && window.epi.publish) {
    // Publishing the "beta/domUpdated" event will tell episerver UI that the dom has changed
    // and that it needs to remap its overlays.
    window.epi.publish("beta/domUpdated");
  }
};

var observeEpiProperties = function (target) {
  const ignoreList = ["#text", "#comment"];
  if (ignoreList.includes(target.nodeName)) {
    return;
  }
  
  var nodes;

  if (target.attributes["data-epi-property-name"]) {
    nodes = [target];
  } else {
    nodes = target.querySelectorAll("[data-epi-property-name]");
  }

  nodes.forEach(function (node) {
    publishEvent()
    var observer = new MutationObserver(publishEvent);
    observer.observe(node, {
      attributes: true,
      attributeFilter: ["data-epi-property-name"]
    });
  });
};

var onNodeAdded = function(mutations) {
  mutations.forEach(function (mutation) {
    mutation.addedNodes.forEach(observeEpiProperties);
  });
};

observeEpiProperties(document.body);

var observer = new MutationObserver(onNodeAdded);
observer.observe(document.body, {
    childList: true,
    subtree: true
});
