// See https://aka.ms/new-console-template for more information
using Sandpit.ConcatenateQueryables;

var _A = new[] { 10, 1, 5 }.AsQueryable();
var _B = new[] { 9, 3, 7 }.AsQueryable();

var _Items = _A.Concatenate(_B).Where(x => x > 3).Select(x => x.ToString()).OrderBy(x => x);
var _ItemsList = _Items.ToList();

var _Items2 = _A.Concatenate(_B).Take(1);
var _Items2List = _Items2.ToList();

_ = 0;