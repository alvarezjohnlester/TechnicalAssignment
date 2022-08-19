using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTieraTechnicalAssignment.Models;
using TechTieraTechnicalAssignment.Models.DTO;

namespace TechTieraTechnicalAssignment.Mapper
{
	public class TransactionProfile : Profile
	{
		public TransactionProfile()
		{
			CreateMap<TransactionData, TransactionDTO>()
				.ForMember(dest => dest.Payment, opt => opt.MapFrom(x => $"{x.Amount.ToString()} {x.CurrencyCode}"))
				.ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) =>
				{
					string status= "";
					switch (src.status.ToLower().Trim())
					{
						case "approved":
							status = "A";
							break;
						case "failed":
							status = "R";
							break;
						case "rejected":
							status = "R";
							break;
						case "finished":
							status = "D";
							break;
						case "done":
							status = "D";
							break;
					}
					return status;
				}));
		}
	}
}
