using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoChromeFix.Models
{
  public abstract class BaseViewModel
  {
    public string TitoloPagina { get; set; }
    public string Testo { get; set; }
    public bool IsSuccess { get; set; }
  }

  public class Page1ViewModel : BaseViewModel
  {
    public Page1ViewModel()
    {
      IsSuccess = true;
    }
  }

  public class Page2ViewModel : BaseViewModel
  {
    public Page2ViewModel()
    {
      IsSuccess = true;
    }
  }
}