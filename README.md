# Winform-Progress-Trackers


Winform Progress Trackers

My main design paradigm for this project, is to try and minimize the re-drawing of the objects as much as possible. i.e. use existing windows objects to control placement, Font usage, spacing, and only recreate images if a control re-size happens.

 

This project is going to take the useful indicators that are becoming common place on the web, and make them into dynamic controls for use in C# Winfom controls.

It will (eventually) have demo projects showing usage; and a simple HowTo to extend projects like MBG.SimpleWizard to create easy to re-use controls for wizard steps, which will then auto populate (And hence progress) the Tracker as a single entity.

So here is a picture of a test App:

It is showing that progress has got to what is called “Page2b”, and it’s style has a border (The other pages did not) and it has a few Winform objects on it that are responding to mouse over.

The node name is derived from the Pages name, and the initialisation (or override) is all done in the parent form that hosts the Wizard tracker.
