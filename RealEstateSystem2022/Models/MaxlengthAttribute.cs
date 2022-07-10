using System;

namespace RealEstateSystem2022.Models
{
    internal class MaxlengthAttribute : Attribute
    {
        private int v;

        public MaxlengthAttribute(int v)
        {
            this.v = v;
        }
    }
}