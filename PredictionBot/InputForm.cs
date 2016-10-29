using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;

namespace PredictionBot
{
    [Serializable]
    public class InputForm
    {
        //   public double WallArea;
        [Prompt("Enter the {&} in square meters. Example: 1.5")]
        public double RoofArea;
        [Prompt("Enter the {&} in meters. Example: 1")]
        public double OverallHeight;
        [Prompt("Enter the {&} in square meters. Example: 2.3")]
        public double GlazingArea;
        [Prompt("Enter the {&} in Kilo watts. Example: 15")]
        public double HeatingLoad;

        public static string strRet1 = string.Empty;

        public static IForm<InputForm> BuildForm()
        {

            
            OnCompletionAsyncDelegate<InputForm> processValues = async (context, state) =>
            {
                await context.PostAsync("Doing the magic..hold on!");
                string strRet = await Program.InvokeRequestResponseService(strRet1);

                await context.PostAsync("The predicted Wall Area is " + strRet + " square meters");
                await context.PostAsync("Type 'Quit' to leave or 'Reset' to star over again!");
            };

            return new FormBuilder<InputForm>()
                    .Message("Hello. How are you? This is Leonardo, a prediction bot! I will predict the Total Wall Area of your room based on Roof Area, Overall Height, Glazing Area and Heating Load.")
                    .Message("I predict by crunching lot of data and applying some really cool machine learning techniques.")
                    .Message("Type 'Quit' to start over at anytime or 'Help' for more options.")


                             .Field(nameof(InputForm.RoofArea),
                            validate: async (state, response) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = response };
                                var value = response.ToString();
                                if (value.Length > 0 && (value[0] < '0' || value[0] > '9'))
                                {
                                    result.Feedback = "Value must start with a number.";
                                    result.IsValid = false;
                                }
                                strRet1 = response.ToString();
                                return result;
                            })

                                 .Field(nameof(InputForm.OverallHeight),
                            validate: async (state, response) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = response };
                                var value = response.ToString();
                                if (value.Length > 0 && (value[0] < '0' || value[0] > '9'))
                                {
                                    result.Feedback = "Value must start with a number.";
                                    result.IsValid = false;
                                }
                                strRet1 += "," + response.ToString();
                                return result;
                            })

                                 .Field(nameof(InputForm.GlazingArea),
                            validate: async (state, response) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = response };
                                var value = response.ToString();
                                if (value.Length > 0 && (value[0] < '0' || value[0] > '9'))
                                {
                                    result.Feedback = "Value must start with a number.";
                                    result.IsValid = false;
                                }
                                strRet1 += "," + response.ToString();
                                return result;
                            })
							
                                 .Field(nameof(InputForm.HeatingLoad),
                            validate: async (state, response) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = response };
                                var value = response.ToString();
                                if (value.Length > 0 && (value[0] < '0' || value[0] > '9'))
                                {
                                    result.Feedback = "Value must start with a number.";
                                    result.IsValid = false;
                                }
                                strRet1 += "," + response.ToString();
                                return result;
                            })


                    .OnCompletion(processValues)
                    .Build();
					
        }

    };
}